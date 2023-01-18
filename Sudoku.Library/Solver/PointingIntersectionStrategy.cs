using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public class PointingIntersectionStrategy : BaseStrategy, ISolverStrategy
    {
        private class PointingIntersectionPosition : Position
        {
            public IRegion PrimaryBlock { get; set; }
            public IRegion IntersectingRegion { get; set; }
            public int Candidate { get; set; }
        }

        public override Position Find(Grid grid)
        {
            foreach (IRegion block in grid.Blocks)
            {
                IEnumerable<RegionCandidateDetails> detailsList = CreateRegionCandidateDetailsForRegion(block);

                Func<RegionCandidateDetails, bool> filterPredicate =
                    x => (x.OccurrenceCount == 2 || x.OccurrenceCount == 3)
                            && (x.BlockCandidateIsLocatedInOneRow() || x.BlockCandidateIsLocatedInOneColumn());

                var intersectionDetails = detailsList.Where(filterPredicate);

                if (intersectionDetails.Any())
                {
                    RegionCandidateDetails intersectionCandidate = intersectionDetails.First();

                    int baseRowIndex = (block.RegionNumber - 1) / 3 * 3;
                    int baseColIndex = (block.RegionNumber - 1) % 3 * 3;

                    IRegion intersectingRegion;
                    int[] cellIndexesToIgnore;

                    if (intersectionCandidate.BlockCandidateIsLocatedInOneRow())
                    {
                        // is row
                        int offset = intersectionCandidate.GetBlockCandidateRowOffset();
                        int rowIndex = baseRowIndex + offset;
                        intersectingRegion = grid.Rows[rowIndex];
                        cellIndexesToIgnore = new int[] { baseColIndex, baseColIndex + 1, baseColIndex + 2 };
                    }
                    else
                    {
                        // is column
                        int offset = intersectionCandidate.GetBlockCandidateColumnOffset();
                        int colIndex = baseColIndex + offset;
                        intersectingRegion = grid.Columns[colIndex];
                        cellIndexesToIgnore = new int[] { baseRowIndex, baseRowIndex + 1, baseRowIndex + 2 };
                    }

                    for (int cellIndex = 0; cellIndex < Grid.GridSize; cellIndex++)
                    {
                        if (cellIndexesToIgnore.Contains(cellIndex))
                            continue;

                        if (intersectingRegion.Cells[cellIndex].Candidates.Contains(intersectionCandidate.CandidateNumber))
                        {
                            return new PointingIntersectionPosition()
                            {
                                PrimaryBlock = block,
                                IntersectingRegion = intersectingRegion,
                                Candidate = intersectionCandidate.CandidateNumber,
                            };
                        }
                    }
                }
            }

            return null;
        }

        public override SolverStep Apply(Grid grid, Position position)
        {
            var p = position as PointingIntersectionPosition;

            if (p == null)
                throw new ArgumentOutOfRangeException(nameof(position), "position is wrong type");

            PointingIntersectionStep changedStep = CreateChangedStep(grid, p);

            foreach (Cell cell in p.IntersectingRegion.EmptyCells)
            {
                if (cell.Block == p.PrimaryBlock)
                    continue;

                if (cell.ClearCandidate(p.Candidate))
                {
                    changedStep.ClearedCandidates.Add(new ClearedCandidate()
                    {
                        Candidate = p.Candidate,
                        Location = MakeCellCoordinate(cell),
                        ContextRegionType = MapRegionType(p.IntersectingRegion),
                        ContextRegionNumber = p.IntersectingRegion.RegionNumber
                    });
                }
            }

            changedStep.GridAfter = CaptureGridState(grid);

            return changedStep;
        }

        private PointingIntersectionStep CreateChangedStep(Grid grid, PointingIntersectionPosition position)
        {
            return new PointingIntersectionStep()
            {
                PrimaryRegionBlockNumber = position.PrimaryBlock.RegionNumber,
                OtherRegionType = MapRegionType(position.IntersectingRegion),
                OtherRegionNumber = position.IntersectingRegion.RegionNumber,
                Candidate = position.Candidate,
                GridBefore = CaptureGridState(grid)
            };
        }

        protected override SolverStep CreateNotFoundStep(Grid grid)
        {
            return new PointingIntersectionNotFoundStep()
            {
                GridBefore = CaptureGridState(grid)
            };
        }
    }
}
