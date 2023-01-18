using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public class GridSolver
    {
        public GridSolver(ISolverStrategy[] solverStrategies)
        {
            _solverStrategies = solverStrategies;
        }

        private readonly ISolverStrategy[] _solverStrategies;

        public IReadOnlyList<SolverStep> Solve(Grid grid)
        {
            var steps = new List<SolverStep>();

            bool changedGrid;

            changedGrid = AssumeAllCandidates(grid, steps);

            if (RemoveInvalidCandidates(grid, steps))
                changedGrid = true;

            // While puzzle not solved and still making progress...
            while (grid.EmptyCells.Any() && changedGrid)
            {
                // for each rule type in simplest to most complex order:
                foreach (ISolverStrategy strategy in _solverStrategies)
                {
                    // try to apply the rule
                    changedGrid = strategy.TryApply(grid, steps);

                    if (changedGrid)
                        break;
                }
            }

            return steps;
        }

        public bool AssumeAllCandidates(Grid grid, List<SolverStep> steps)
        {
            var candidates = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var changedCells = new List<CellCoordinate>();

            CellState[,] gridBefore = Utilities.CaptureGridState(grid);

            foreach (var cell in grid.EmptyCells)
            {
                cell.SetCandidates(candidates);
                changedCells.Add(Utilities.MakeCellCoordinate(cell));
            }

            if (changedCells.Count > 0)
            {
                var changedStep = new AssumeCandidatesStep()
                {
                    GridBefore = gridBefore,
                    GridAfter = Utilities.CaptureGridState(grid)
                };
                changedStep.Locations.AddRange(changedCells);
                steps.Add(changedStep);
                return true;
            }
            else
            {
                var notFoundStep = new AssumeCandidatesNotFoundStep()
                {
                    GridBefore = gridBefore
                };
                steps.Add(notFoundStep);
                return false;
            }
        }

        public bool RemoveInvalidCandidates(Grid grid, List<SolverStep> steps)
        {
            bool changedGrid = false;
            foreach (var filledCell in grid.FilledCells)
            {
                var changedStep = new RemoveInvalidCandidatesStep()
                {
                    Location = Utilities.MakeCellCoordinate(filledCell),
                    Number = filledCell.Number.Value,
                    GridBefore = Utilities.CaptureGridState(grid)
                };

                bool changed = Utilities.ClearCandidatesInFilledCellRegions(filledCell, changedStep.ClearedCandidates);

                if (changed)
                {
                    changedGrid = true;

                    changedStep.GridAfter = Utilities.CaptureGridState(grid);
                    steps.Add(changedStep);
                }
            }

            if (!changedGrid)
            {
                var notFoundStep = new RemoveInvalidCandidatesNotFoundStep()
                {
                    GridBefore = Utilities.CaptureGridState(grid)
                };
                steps.Add(notFoundStep);
            }

            return changedGrid;
        }
    }
}
