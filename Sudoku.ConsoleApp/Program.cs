using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.Puzzles;
using Sudoku.Library.Solver;
using Sudoku.Library.SolverSteps;

namespace Sudoku.ConsoleApp
{
    public static class Program
    {

        static void Main(string[] args)
        {
            var builder = new GridBuilder();

            var solverStrategies = new ISolverStrategy[]
            {
                new NakedSingleStrategy(),
                new NakedDoubleStrategy(),
                new HiddenSingleStrategy(),
                new HiddenDoubleStrategy(),
                new PointingIntersectionStrategy(),
                new ClaimingIntersectionStrategy(),
                new NakedTripleStrategy(),
                new XWingStrategy()
            };

            var solver = new GridSolver(solverStrategies);
            var gridView = new GridConsoleView();
            var logView = new LogConsoleView();

            Console.WriteLine("Building puzzle");
            var grid = builder.Build(PuzzleId.Tricky750);

            Console.WriteLine("\nPuzzle is:");
            RenderGrid(gridView, grid);

            // -------

            Console.WriteLine("\nSolving puzzle");

            var log = solver.Solve(grid);
            RenderLog(logView, log);

            // -------

            Console.WriteLine("\nFinished solving:");
            RenderGrid(gridView, grid);

            if (grid.EmptyCells.Any())
                Console.WriteLine("Puzzle NOT solved... :-/");
            else
                Console.WriteLine("Puzzle SOLVED!!! :-)");

            Console.WriteLine($"\nAll Regions Valid: {grid.AllRegionsAreValid}");

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void RenderGrid(GridConsoleView view, Grid grid)
        {
            foreach (var line in view.Render(grid))
                Console.WriteLine(line);
        }

        private static void RenderLog(LogConsoleView view, IReadOnlyList<SolverStep> log)
        {
            foreach (var msg in view.Render(log))
                Console.WriteLine(msg);
        }
    }
}
