using System.Collections.Generic;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    /// <summary>
    /// Common interface for solving strategies used by GridSolver
    /// </summary>
    public interface ISolverStrategy
    {
        bool TryApply(Grid grid, List<SolverStep> steps);
    }
}
