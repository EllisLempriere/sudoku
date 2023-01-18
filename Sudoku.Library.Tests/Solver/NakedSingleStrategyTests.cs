using System.Collections.Generic;
using NUnit.Framework;
using Sudoku.Library.Game;
using Sudoku.Library.Solver;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Tests.Solver
{
    [TestFixture]
    public class NakedSingleStrategyTests
    {
        [Test]
        public void TryApply_OneNakedSingle_Resolved()
        {
            // arrange
            var grid = CreateGridWithOneNakedSingle();
            var strategy = new NakedSingleStrategy();
            var steps = new List<SolverStep>();

            // act
            bool actualChangedGrid = strategy.TryApply(grid, steps);

            // assert
            Assert.IsTrue(actualChangedGrid);
            Assert.AreEqual(9, grid.Cells[2, 1].Number.Value);
            Assert.IsTrue(grid.AllRegionsAreValid);
        }

        private Grid CreateGridWithOneNakedSingle()
        {
            var grid = CreateGrid(_veryEasy002SolvedData);

            grid.Cells[2, 1].Number = null;
            grid.Cells[2, 1].SetCandidate(9);

            Assume.That(grid.AllRegionsAreValid);

            return grid;
        }

        private Grid CreateGrid(int[,] data)
        {
            var grid = new Grid();

            for (int row = 0; row <= data.GetUpperBound(0); row++)
                for (int col = 0; col <= data.GetUpperBound(1); col++)
                {
                    if (data[row, col] > 0)
                        grid.Cells[row, col].Number = data[row, col];
                }

            Assume.That(grid.AllRegionsAreValid);

            return grid;
        }

        private readonly int[,] _veryEasy002SolvedData = new[,]
        {
            { 7, 2, 1,   6, 8, 5,   4, 9, 3 },
            { 3, 8, 6,   1, 9, 4,   5, 2, 7 },
            { 5, 9, 4,   2, 7, 3,   6, 8, 1 },

            { 8, 4, 5,   7, 6, 2,   1, 3, 9 },
            { 9, 6, 3,   8, 5, 1,   2, 7, 4 },
            { 1, 7, 2,   3, 4, 9,   8, 5, 6 },

            { 6, 5, 9,   4, 2, 7,   3, 1, 8 },
            { 2, 1, 8,   9, 3, 6,   7, 4, 5 },
            { 4, 3, 7,   5, 1, 8,   9, 6, 2 }
        };

    }
}
