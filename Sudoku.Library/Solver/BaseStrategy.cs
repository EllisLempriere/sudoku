using System.Collections.Generic;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public abstract class BaseStrategy
    {
        public abstract class Position
        {
        }

        public bool TryApply(Grid grid, List<SolverStep> steps)
        {
            Position position = Find(grid);

            SolverStep step;

            if (position != null)
            {
                step = Apply(grid, position);
            }
            else
            {
                step = CreateNotFoundStep(grid);
            }

            steps.Add(step);

            return position != null;
        }

        public abstract Position Find(Grid grid);

        public abstract SolverStep Apply(Grid grid, Position position);

        protected abstract SolverStep CreateNotFoundStep(Grid grid);     
        
        protected CellState[,] CaptureGridState(Grid grid)
        {
            return Utilities.CaptureGridState(grid);
        }

        protected static CellCoordinate MakeCellCoordinate(Cell cell)
        {
            return Utilities.MakeCellCoordinate(cell);
        }

        protected static RegionType MapRegionType(IRegion region)
        {
            return Utilities.MapRegionType(region);
        }

        // TODO: Linquify callers and remove method
        protected IEnumerable<RegionCandidateDetails> CreateRegionCandidateDetailsForRegion(IRegion region)
        {
            for (int candidate = 1; candidate <= 9; candidate++)
            {
                yield return new RegionCandidateDetails(region, candidate);
            }
        }
    }
}

