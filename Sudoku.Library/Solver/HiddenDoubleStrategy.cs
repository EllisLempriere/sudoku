using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public class HiddenDoubleStrategy : BaseStrategy, ISolverStrategy
    {

        private class HiddenDoublePosition : Position
        {
            public IRegion Region;
            public Cell FirstCell;
            public Cell SecondCell;
            public int FirstCandidate;
            public int SecondCandidate;
        };

        public override Position Find(Grid grid)
        {
            foreach (IRegion region in grid.Regions)
            {
                IEnumerable<RegionCandidateDetails> detailsList = CreateRegionCandidateDetailsForRegion(region);
                var hiddenDoubleGroups = detailsList
                                        .Where(x => x.OccurrenceCount == 2)
                                        .GroupBy(x => x.CellIndexPattern)
                                        .Where(x => x.Count() == 2);

                if (hiddenDoubleGroups.Any())
                {
                    var group = hiddenDoubleGroups.First();
                    RegionCandidateDetails details1 = group.ElementAt(0);
                    RegionCandidateDetails details2 = group.ElementAt(1);

                    var candidatesToKeep = new HashSet<int>(new int[] { details1.CandidateNumber, details2.CandidateNumber });

                    foreach (var index in details1.CellIndexes)
                    {
                        Cell c = region.Cells[index];

                        var candidatesToClear = new HashSet<int>(c.Candidates);
                        candidatesToClear.ExceptWith(candidatesToKeep);

                        if (candidatesToClear.Count > 0)
                        {
                            return new HiddenDoublePosition()
                            {
                                Region = region,
                                FirstCell = region.Cells[details1.CellIndexes.ElementAt(0)],
                                SecondCell = region.Cells[details1.CellIndexes.ElementAt(1)],
                                FirstCandidate = details1.CandidateNumber,
                                SecondCandidate = details2.CandidateNumber
                            };
                        }
                    }
                }
            }

            return null;
        }

        public override SolverStep Apply(Grid grid, Position position)
        {
            var p = position as HiddenDoublePosition;

            if (p == null)
                throw new ArgumentOutOfRangeException(nameof(position), "Wrong position type");

            HiddenDoubleStep step = CreateChangedStep(grid, p);

            var candidatesToKeep = new HashSet<int>(new int[] { p.FirstCandidate, p.SecondCandidate });

            Cell[] cells = new Cell[] { p.FirstCell, p.SecondCell };
            foreach (Cell c in cells)
            {
                var candidatesToClear = new HashSet<int>(c.Candidates);
                candidatesToClear.ExceptWith(candidatesToKeep);

                if (candidatesToClear.Count > 0)
                {
                    c.ClearCandidates(candidatesToClear);

                    foreach (int candidate in candidatesToClear)
                    {
                        step.ClearedCandidates.Add(new ClearedCandidate()
                        {
                            Candidate = candidate,
                            ContextRegionType = MapRegionType(p.Region),
                            ContextRegionNumber = p.Region.RegionNumber,
                            Location = MakeCellCoordinate(c)
                        });
                    }
                }
            }

            step.GridAfter = CaptureGridState(grid);
            return step;
        }

        private HiddenDoubleStep CreateChangedStep(Grid grid, HiddenDoublePosition position)
        {
            var step = new HiddenDoubleStep()
            {
                PrimaryRegionType = MapRegionType(position.Region),
                PrimaryRegionNumber = position.Region.RegionNumber,
                GridBefore = CaptureGridState(grid)
            };

            step.Locations[0] = MakeCellCoordinate(position.FirstCell);
            step.Locations[1] = MakeCellCoordinate(position.SecondCell);

            step.Candidates[0] = position.FirstCandidate;
            step.Candidates[1] = position.SecondCandidate;

            return step;
        }

        protected override SolverStep CreateNotFoundStep(Grid grid)
        {
            return new HiddenDoubleNotFoundStep()
            {
                GridBefore = CaptureGridState(grid)
            };
        }
    }
}
