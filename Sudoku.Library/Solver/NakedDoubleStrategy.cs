using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public class NakedDoubleStrategy : BaseStrategy, ISolverStrategy
    {
        private class NakedDoublePosition : Position
        {
            public IRegion Region;
            public Cell FirstCell;
            public Cell SecondCell;

        };

        public override Position Find(Grid grid)
        {
            foreach (IRegion region in grid.Regions)
            {
                foreach (Cell c in region.EmptyCells)
                {
                    if (c.Candidates.Count() == 2)
                    {
                        foreach (Cell d in region.EmptyCells)
                        {
                            if (d == c)
                                continue;

                            if (d.Candidates.SequenceEqual(c.Candidates))
                            {
                                var cellsToClear = region.EmptyCells.Where(x => x != c && x != d && x.Candidates.Intersect(c.Candidates).Any());

                                if (cellsToClear.Any())
                                {
                                    return new NakedDoublePosition()
                                    {
                                        Region = region,
                                        FirstCell = c,
                                        SecondCell = d
                                    };
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        public override SolverStep Apply(Grid grid, Position position)
        {
            var p = position as NakedDoublePosition;

            if (p == null)
                throw new ArgumentOutOfRangeException(nameof(position), "Position is wrong type");

            NakedDoubleStep step = CreateChangedStep(grid, p);

            var cellsToClear = p.Region.EmptyCells.Where(x => 
                        x != p.FirstCell 
                        && x != p.SecondCell 
                        && x.Candidates.Intersect(p.FirstCell.Candidates).Any());

            foreach (var cell in cellsToClear)
            {
                foreach (int candidate in p.FirstCell.Candidates)
                {
                    if (cell.ClearCandidate(candidate))
                    {
                        step.ClearedCandidates.Add(new ClearedCandidate()
                        {
                            Candidate = candidate,
                            ContextRegionType = MapRegionType(p.Region),
                            ContextRegionNumber = p.Region.RegionNumber,
                            Location = MakeCellCoordinate(cell)
                        });
                    }
                }
            }

            step.GridAfter = CaptureGridState(grid);
            return step;
        }

        private NakedDoubleStep CreateChangedStep(Grid grid, NakedDoublePosition position)
        {
            var step = new NakedDoubleStep()
            {
                PrimaryRegionType = MapRegionType(position.Region),
                PrimaryRegionNumber = position.Region.RegionNumber,
                GridBefore = CaptureGridState(grid)
            };

            step.Locations[0] = MakeCellCoordinate(position.FirstCell);
            step.Locations[1] = MakeCellCoordinate(position.SecondCell);

            step.Candidates[0] = position.FirstCell.Candidates.ElementAt(0);
            step.Candidates[1] = position.FirstCell.Candidates.ElementAt(1);

            return step;
        }

        protected override SolverStep CreateNotFoundStep(Grid grid)
        {
            return new NakedDoubleNotFoundStep()
            {
                GridBefore = CaptureGridState(grid)
            };
        }
    }
}
