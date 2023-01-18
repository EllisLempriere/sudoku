using System;
using System.Linq;

namespace Sudoku.Library.SolverSteps
{
    /// <summary>
    /// Ancestor of all SolverSteps that change the grid
    /// </summary>
    public abstract class ChangingSolverStep : SolverStep
    {
        /// <summary>
        /// State of grid after rule was applied
        /// </summary>
        public CellState[,] GridAfter { get; set; }

        public override bool Equals(object obj)
        {
            var step = obj as ChangingSolverStep;

            if (step == null || !base.Equals(obj))
                return false;

            var gridAfterEqual =
                    GridAfter.Rank == step.GridAfter.Rank &&
                    Enumerable.Range(0, GridAfter.Rank).All(
                        dimension => GridAfter.GetLength(dimension) == step.GridAfter.GetLength(dimension)) &&
                    GridBefore.Cast<CellState>().SequenceEqual(step.GridBefore.Cast<CellState>());

            return gridAfterEqual;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), GridAfter);
        }
    }
}
