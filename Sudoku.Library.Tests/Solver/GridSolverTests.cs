using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using Sudoku.Library.Game;
using Sudoku.Library.Solver;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Tests.Solver
{
    [TestFixture]
    public class GridSolverTests
    {
        [Test]
        public void Constructor_Default_ReturnsInstance()
        {
            // arrange

            // act
            var solver = new GridSolver(_standardSolverStrategies);

            // assert
            Assert.IsNotNull(solver);
        }

        [Test]
        public void Solve_Tricky750_ReturnsCorrectLog()
        {
            // arrange
            IReadOnlyList<SolverStep> expectedLog = GetSolverStepsFromFile("Solver/Tricky750_ExpectedSolverSteps.json");
            Grid grid = CreateGrid(_tricky750InitialData);
            var solver = new GridSolver(_standardSolverStrategies);

            // act
            IReadOnlyList<SolverStep> actualLog = solver.Solve(grid);

            SaveSolverStepsToFile(actualLog, "Solve_Tricky750_ReturnsCorrectLog-ActualLog.json");
            OutputUniqueStepNamesToTestContext(actualLog);

            // assert
            Assert.IsFalse(grid.EmptyCells.Any(), "Puzzle was not solved");
            Assert.IsTrue(grid.AllRegionsAreValid, "Solution has error");
            Assert.AreEqual(expectedLog, actualLog, "Unexpected difference in log");
        }

        [Test]
        public void Solve_Tricky753_ReturnsCorrectLog()
        {
            // arrange
            IReadOnlyList<SolverStep> expectedLog = GetSolverStepsFromFile("Solver/Tricky753_ExpectedSolverSteps.json");
            Grid grid = CreateGrid(_tricky753InitialData);
            var solver = new GridSolver(_standardSolverStrategies);

            // act
            IReadOnlyList<SolverStep> actualLog = solver.Solve(grid);

            SaveSolverStepsToFile(actualLog, "Solve_Tricky753_ReturnsCorrectLog-ActualLog.json");
            OutputUniqueStepNamesToTestContext(actualLog);

            // assert
            Assert.IsFalse(grid.EmptyCells.Any(), "Puzzle was not solved");
            Assert.IsTrue(grid.AllRegionsAreValid, "Solution has error");
            Assert.AreEqual(expectedLog, actualLog, "Unexpected difference in log");
        }

        #region Private Methods
        
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

        private IReadOnlyList<SolverStep> GetSolverStepsFromFile(string filePathName)
        {
            string jsonText = File.ReadAllText(filePathName);
            var log = JsonConvert.DeserializeObject<IReadOnlyList<SolverStep>>(jsonText, _jsonSerializerSettings);
            return log;
        }

        private void SaveSolverStepsToFile(IReadOnlyList<SolverStep> actualLog, string filePathName)
        {
            string jsonText = JsonConvert.SerializeObject(actualLog, Formatting.Indented, _jsonSerializerSettings);
            File.WriteAllText(filePathName, jsonText);
        }

        private void OutputUniqueStepNamesToTestContext(IReadOnlyList<SolverStep> actualLog)
        {
            var uniqueSteps = actualLog.Select(x => x.GetType().Name).Distinct().OrderBy(x => x);
            foreach (var name in uniqueSteps)
                TestContext.WriteLine(name);
        }
        #endregion

        private readonly ISolverStrategy[] _standardSolverStrategies = new ISolverStrategy[]
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

#pragma warning disable S1144 // Unused, but keep for now
        private readonly int[,] _veryEasy002InitialData = new[,]
        {
            { 0, 2, 1,   6, 0, 0,   4, 9, 0 },
            { 3, 8, 0,   1, 9, 4,   0, 0, 0 },
            { 5, 0, 0,   0, 7, 0,   6, 0, 0 },

            { 0, 4, 5,   7, 0, 2,   1, 0, 0 },
            { 9, 6, 0,   0, 5, 0,   0, 7, 4 },
            { 0, 0, 2,   3, 0, 9,   8, 5, 0 },

            { 0, 0, 9,   0, 2, 0,   0, 0, 8 },
            { 0, 0, 0,   9, 3, 6,   0, 4, 5 },
            { 0, 3, 7,   0, 0, 8,   9, 6, 0 }
        };
#pragma warning restore S1144

        private readonly int[,] _tricky750InitialData = new[,]
        {
            { 4, 0, 7,   0, 2, 0,   0, 6, 1},
            { 0, 8, 2,   0, 6, 0,   0, 4, 0},
            { 0, 0, 0,   0, 0, 0,   0, 0, 0},

            { 2, 4, 0,   9, 0, 0,   0, 0, 7},
            { 1, 0, 0,   2, 0, 3,   0, 0, 9},
            { 7, 0, 0,   0, 0, 6,   0, 1, 2},

            { 0, 0, 0,   0, 0, 0,   0, 0, 0},
            { 0, 2, 0,   0, 3, 0,   5, 7, 0},
            { 3, 6, 0,   0, 4, 0,   1, 0, 8}
        };

#pragma warning disable S1144 // Unused, but keep for now
        private readonly int[,] _tricky752InitialData = new[,]
        {
            { 0, 4, 0,   0, 0, 7,   0, 0, 0 },
            { 0, 8, 0,   4, 0, 0,   1, 9, 0 },
            { 9, 0, 7,   1, 0, 0,   5, 0, 0 },

            { 8, 5, 3,   2, 0, 0,   7, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 2,   0, 0, 5,   3, 4, 8 },

            { 0, 0, 8,   0, 0, 4,   6, 0, 5 },
            { 0, 6, 4,   0, 0, 2,   0, 1, 0 },
            { 0, 0, 0,   8, 0, 0,   0, 2, 0 }
        };
#pragma warning restore S1144 

        private readonly int[,] _tricky753InitialData = new[,]
        {
            { 4, 6, 0,   0, 0, 0,   1, 0, 0 },
            { 8, 0, 0,   0, 9, 0,   4, 0, 7 },
            { 2, 0, 5,   4, 0, 0,   0, 9, 0 },

            { 0, 0, 8,   0, 4, 0,   0, 0, 0 },
            { 0, 0, 0,   9, 0, 5,   0, 0, 0 },
            { 0, 0, 0,   0, 1, 0,   5, 0, 0 },

            { 0, 5, 0,   0, 0, 7,   2, 0, 6 },
            { 3, 0, 6,   0, 2, 0,   0, 0, 4 },
            { 0, 0, 7,   0, 0, 0,   0, 8, 9 }
        };

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };
    }
}
