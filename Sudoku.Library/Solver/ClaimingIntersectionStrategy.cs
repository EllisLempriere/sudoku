using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public class ClaimingIntersectionStrategy : BaseStrategy, ISolverStrategy
    {
        private class ClaimingIntersectionPosition : Position
        {
            public IRegion PrimaryRegion { get; set; }
            public bool PrimaryRegionIsRow => PrimaryRegion is GridRow;
            public IRegion IntersectingBlock { get; set; }
            public int Candidate { get; set; }
        }

        public override Position Find(Grid grid)
        {
            IEnumerable<int> candidates = Enumerable.Range(1, 9);
            IEnumerable<IRegion> rowsAndColumns = grid.Rows.Cast<IRegion>().Concat(grid.Columns.Cast<IRegion>());

            IEnumerable<RegionCandidateDetails> candidateRegionDetails = rowsAndColumns.SelectMany(r => candidates.Select(c => new RegionCandidateDetails(r, c)));

            var claimingIntersectionDetails = candidateRegionDetails.FirstOrDefault(rcd =>
                    (rcd.OccurrenceCount == 2 || rcd.OccurrenceCount == 3) 
                    && rcd.RowColumnCandidateIsLocatedInOneBlock()
                    && ClaimingIntersectionIsActionable(rcd, grid)
                );

            if (claimingIntersectionDetails != null)
            {
                return new ClaimingIntersectionPosition()
                {
                    PrimaryRegion = claimingIntersectionDetails.Region,
                    IntersectingBlock = FindIntersectingBlock(claimingIntersectionDetails, grid),
                    Candidate = claimingIntersectionDetails.CandidateNumber
                };
            }
            else
            {
                return null;
            }
        }

        private bool ClaimingIntersectionIsActionable(RegionCandidateDetails claimingIntersectionDetails, Grid grid)
        {
            int regionIndex = claimingIntersectionDetails.Region.RegionNumber - 1;
            bool isRow = claimingIntersectionDetails.Region is GridRow;

            IRegion intersectingBlock = FindIntersectingBlock(claimingIntersectionDetails, grid);

            return intersectingBlock.EmptyCells.Any(
                c =>
                    c.IsCandidateSet(claimingIntersectionDetails.CandidateNumber)
                    &&
                    (
                        (isRow && c.Location.RowIndex != regionIndex)
                        || (!isRow && c.Location.ColIndex != regionIndex)
                    )
                );
        }

        private IRegion FindIntersectingBlock(RegionCandidateDetails claimingIntersectionDetails, Grid grid)
        {
            int regionIndex = claimingIntersectionDetails.Region.RegionNumber - 1;
            bool isRow = claimingIntersectionDetails.Region is GridRow;

            int baseBlockIndex;
            int blockIndex;

            if (isRow)
            {
                baseBlockIndex = regionIndex / 3 * 3;
                blockIndex = baseBlockIndex + claimingIntersectionDetails.GetRowColumnCandidateBlockOffset();
            }
            else
            {
                baseBlockIndex = claimingIntersectionDetails.GetRowColumnCandidateBlockOffset() * 3;
                blockIndex = baseBlockIndex + (regionIndex / 3);
            }

            return grid.Blocks[blockIndex];
        }

        public override SolverStep Apply(Grid grid, Position position)
        {
            var p = position as ClaimingIntersectionPosition;

            if (p == null)
                throw new ArgumentOutOfRangeException(nameof(position), "Wrong type of position");

            ClaimingIntersectionStep step = CreateChangedStep(grid, p);

            foreach (var cell in p.IntersectingBlock.EmptyCells)
            {
                if (p.PrimaryRegionIsRow)
                {
                    if (cell.Location.RowIndex == p.PrimaryRegion.RegionNumber - 1)
                        continue; // skip block cells that are in the Primary Region (row)
                }
                else
                {
                    if (cell.Location.ColIndex == p.PrimaryRegion.RegionNumber - 1)
                        continue; // skip block cells that are in the Primary Region (column)
                }

                if (cell.ClearCandidate(p.Candidate))
                {
                    step.ClearedCandidates.Add(new ClearedCandidate()
                    {
                        Candidate = p.Candidate,
                        Location = MakeCellCoordinate(cell),
                        ContextRegionType = MapRegionType(p.IntersectingBlock),
                        ContextRegionNumber = p.IntersectingBlock.RegionNumber
                    });
                }
            }

            step.GridAfter = CaptureGridState(grid);

            return step;
        }

        private ClaimingIntersectionStep CreateChangedStep(Grid grid, ClaimingIntersectionPosition position)
        {
            return new ClaimingIntersectionStep()
            {
                PrimaryRegionType = MapRegionType(position.PrimaryRegion),
                PrimaryRegionNumber = position.PrimaryRegion.RegionNumber,
                OtherRegionBlockNumber = position.IntersectingBlock.RegionNumber,
                Candidate = position.Candidate,
                GridBefore = CaptureGridState(grid)
            };
        }

        protected override SolverStep CreateNotFoundStep(Grid grid)
        {
            return new ClaimingIntersectionNotFoundStep()
            {
                GridBefore = CaptureGridState(grid)
            };
        }
    }
}
