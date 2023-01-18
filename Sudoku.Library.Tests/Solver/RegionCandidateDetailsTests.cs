using System;
using NSubstitute;
using NUnit.Framework;
using Sudoku.Library.Game;
using Sudoku.Library.Solver;

namespace Sudoku.Library.Tests.Solver
{
    [TestFixture]
    public class RegionCandidateDetailsTests
    {
        [Test]
        public void Constructor_AllCandidateInAllCells_ReturnsCorrectValues()
        {
            // arrange
            int candidateNumber = 1;

            IRegion mockRegion = Substitute.For<IRegion>();
            Cell[] regionCells = CreateRegionCells_AllCandidates(RegionType.Row);
            mockRegion.Cells.Returns(regionCells.AsReadOnly());

            int expectedCellIndexPattern = 511; // binary 0001 1111 1111

            // act
            RegionCandidateDetails rcd = new RegionCandidateDetails(mockRegion, candidateNumber);

            // assert
            Assert.NotNull(rcd);
            Assert.AreEqual(candidateNumber, rcd.CandidateNumber);
            Assert.AreEqual(9, rcd.OccurrenceCount);
            Assert.AreEqual(expectedCellIndexPattern, rcd.CellIndexPattern);
            CollectionAssert.AreEqual(Indexes_All, rcd.CellIndexes);
        }

        [Test]
        public void Constructor_HiddenDoubleInRow_ReturnsCorrectValues()
        {
            // arrange
            int hiddenDoubleCandidate = 1;

            // Sample Row Candidate Pattern
            // --3   --3   123  |  --3   --3   --3  |  123   --3   --3
            // 456   456   456  |  456   456   456  |  456   456   456
            // 789   789   789  |  789   789   789  |  789   789   789

            IRegion mockRegion = Substitute.For<IRegion>();
            Cell[] regionCells = CreateRegionCells_HiddenDoubleExample(RegionType.Row);
            mockRegion.Cells.Returns(regionCells.AsReadOnly());

            int expectedOccurrenceCount = 2;
            int expectedCellIndexPattern = 68; // binary 0000 0100 0100 == 64 + 4 == 68
            int[] expectedCellIndexes = new int[] { 2, 6 };

            // act
            RegionCandidateDetails rcd = new RegionCandidateDetails(mockRegion, hiddenDoubleCandidate);

            // assert
            Assert.NotNull(rcd);
            Assert.AreEqual(hiddenDoubleCandidate, rcd.CandidateNumber);
            Assert.AreEqual(expectedOccurrenceCount, rcd.OccurrenceCount);
            Assert.AreEqual(expectedCellIndexPattern, rcd.CellIndexPattern);
            CollectionAssert.AreEqual(expectedCellIndexes, rcd.CellIndexes);
        }

        private readonly int[] Candidates_All = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly int[] Candidates_AllExceptOneTwo = new int[] { 3, 4, 5, 6, 7, 8, 9 };

        private readonly int[] Indexes_All = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        private enum RegionType
        {
            Row,
            Column,
            Block
        };

        private Cell[] CreateRegionCells_AllCandidates(RegionType type = RegionType.Row)
        {
            Cell[] regionCells = new Cell[Grid.GridSize];

            for (int index = 0; index < Grid.GridSize; index++)
            {
                int rowIndex;
                int colIndex;

                switch (type)
                {
                    case RegionType.Row:
                        rowIndex = index;
                        colIndex = 0;
                        break;

                    case RegionType.Column:
                        rowIndex = 0;
                        colIndex = index;
                        break;

                    case RegionType.Block:
                        rowIndex = index / 3;
                        colIndex = index % 3;
                        break;

                    default:
                        throw new NotImplementedException();
                }

                regionCells[index] = new Cell(rowIndex, colIndex);
                regionCells[index].SetCandidates(Candidates_All);
            }

            return regionCells;
        }

        /// <summary>
        /// Construct some region cells with candidates that exhibit a hidden double
        /// </summary>
        /// <remarks>
        /// Sample Row Candidate Pattern
        /// --3   --3   123  |  --3   --3   --3  |  123   --3   --3
        /// 456   456   456  |  456   456   456  |  456   456   456
        /// 789   789   789  |  789   789   789  |  789   789   789
        /// </remarks>
        private Cell[] CreateRegionCells_HiddenDoubleExample(RegionType type = RegionType.Row)
        {
            Cell[] regionCells = new Cell[Grid.GridSize];

            for (int index = 0; index < Grid.GridSize; index++)
            {
                int rowIndex;
                int colIndex;

                switch (type)
                {
                    case RegionType.Row:
                        rowIndex = index;
                        colIndex = 0;
                        break;

                    case RegionType.Column:
                        rowIndex = 0;
                        colIndex = index;
                        break;

                    case RegionType.Block:
                        rowIndex = index / 3;
                        colIndex = index % 3;
                        break;

                    default:
                        throw new NotImplementedException();
                }

                regionCells[index] = new Cell(rowIndex, colIndex);
                regionCells[0].SetCandidates(Candidates_AllExceptOneTwo);
            }

            regionCells[2].SetCandidates(Candidates_All);
            regionCells[6].SetCandidates(Candidates_All);

            return regionCells;
        }
    }
}
