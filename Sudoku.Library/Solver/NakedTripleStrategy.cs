using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public class NakedTripleStrategy : BaseStrategy, ISolverStrategy
    {
        private class NakedTriplePosition : Position
        {
            public IRegion Region;
            public Cell FirstCell;
            public Cell SecondCell;
            public Cell ThirdCell;
            public int FirstCandidate;
            public int SecondCandidate;
            public int ThirdCandidate;
        };

        public override Position Find(Grid grid)
        {
            foreach (IRegion region in grid.Regions)
            {
                // find potential cells
                List<Cell> potentialCells = new List<Cell>();
                foreach (Cell c in region.EmptyCells)
                {
                    int numberOfCandidates = c.Candidates.Count();
                    if (numberOfCandidates == 2 || numberOfCandidates == 3)
                    {
                        potentialCells.Add(c);
                    }
                }

                // create set of all potential candidates for naked triples from potential candidates list
                HashSet<int> candidates = new HashSet<int>();
                foreach (Cell p in potentialCells)
                {
                    candidates.UnionWith(p.Candidates);
                }

                // generate list of 3-candidate possibilities from hash set
                Utilities utils = new Utilities();
                List<HashSet<int>> combinations = utils.GetCombinations(candidates.ToArray(), 3);

                foreach (HashSet<int> combo in combinations)
                {
                    // find cells with candidates that are equal to or a subset of each combination
                    var matchingCells = potentialCells.Where(x => combo.IsSupersetOf(x.Candidates)).ToList();

                    // find count of matching cells
                    int count = matchingCells.Count;

                    if (count == 3)
                    {
                        var cellsToClear = region.EmptyCells.Where(x => !matchingCells.Contains(x) && x.Candidates.Intersect(combo).Any());

                        if (cellsToClear.Any())
                            return new NakedTriplePosition()
                            {
                                Region = region,
                                FirstCell = matchingCells[0],
                                SecondCell = matchingCells[1],
                                ThirdCell = matchingCells[2],
                                FirstCandidate = combo.ElementAt(0),
                                SecondCandidate = combo.ElementAt(1),
                                ThirdCandidate = combo.ElementAt(2)
                            };
                    }
                }
            }

            return null;
        }

        public override SolverStep Apply(Grid grid, Position position)
        {
            var p = position as NakedTriplePosition;

            if (p == null)
                throw new ArgumentOutOfRangeException(nameof(position), "position is wrong type");

            NakedTripleStep step = CreateChangedStep(grid, p);

            var nakedTripleCandidates = new HashSet<int>(new int[] { p.FirstCandidate, p.SecondCandidate, p.ThirdCandidate });

            Cell[] cells = new Cell[] { p.FirstCell, p.SecondCell, p.ThirdCell };

            IEnumerable<Cell> cellsToClear = p.Region.EmptyCells.Where(x => !cells.Contains(x) && x.Candidates.Intersect(nakedTripleCandidates).Any());

            foreach (var cell in cellsToClear)
            {
                foreach (var candidate in nakedTripleCandidates)
                {
                    if (cell.ClearCandidate(candidate))
                    {
                        step.ClearedCandidates.Add(new ClearedCandidate()
                        {
                            Candidate = candidate,
                            Location = MakeCellCoordinate(cell),
                            ContextRegionType = MapRegionType(p.Region),
                            ContextRegionNumber = p.Region.RegionNumber
                        });
                    }
                }
            }

            step.GridAfter = CaptureGridState(grid);

            return step;
        }

        private NakedTripleStep CreateChangedStep(Grid grid, NakedTriplePosition position)
        {
            var step = new NakedTripleStep()
            {
                PrimaryRegionType = MapRegionType(position.Region),
                PrimaryRegionNumber = position.Region.RegionNumber,
                GridBefore = CaptureGridState(grid)
            };

            step.Locations[0] = MakeCellCoordinate(position.FirstCell);
            step.Locations[1] = MakeCellCoordinate(position.SecondCell);
            step.Locations[2] = MakeCellCoordinate(position.ThirdCell);

            step.Candidates[0] = position.FirstCandidate;
            step.Candidates[1] = position.SecondCandidate;
            step.Candidates[2] = position.ThirdCandidate;

            return step;
        }

        protected override SolverStep CreateNotFoundStep(Grid grid)
        {
            return new NakedTripleNotFoundStep()
            {
                GridBefore = CaptureGridState(grid)
            };
        }
    }
}
