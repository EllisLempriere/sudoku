using System;
using System.Linq;

namespace Sudoku.Library.SolverSteps
{
    /// <summary>
    /// Ancestor of all SolverSteps
    /// </summary>
    public abstract class SolverStep
    {
        /// <summary>
        /// State of grid before rule application was attempted
        /// </summary>
        public CellState[,] GridBefore { get; set; }

        public override bool Equals(object obj)
        {
            var step = obj as SolverStep;

            if (step == null)
                return false;

            var gridBeforeEqual =
                    GridBefore.Rank == step.GridBefore.Rank &&
                    Enumerable.Range(0, GridBefore.Rank).All(
                        dimension => GridBefore.GetLength(dimension) == step.GridBefore.GetLength(dimension)) &&
                    GridBefore.Cast<CellState>().SequenceEqual(step.GridBefore.Cast<CellState>());

            return gridBeforeEqual;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GridBefore);
        }
    }
}
