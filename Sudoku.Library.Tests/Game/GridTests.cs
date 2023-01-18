using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sudoku.Library.Game;

namespace Sudoku.Library.Tests.Game
{
    public class GridTests
    {
        private Grid _grid;

        [SetUp]
        public void Setup()
        {
            _grid = new Grid();
        }


        // Property Tests
        // Property: Constructor
        [Test]
        public void Constructor_Default_ReturnsInstance()
        {
            // arrange

            // act
            Grid grid = new Grid();

            // assert
            Assert.IsNotNull(grid);
        }


        // Property: Cells
        [Test]
        public void Cells_Default_Is9x9()
        {
            // arrange
            Grid grid = new Grid();

            // act
            int numOfRows = grid.Cells.GetUpperBound(0);
            int numOfColumns = grid.Cells.GetUpperBound(1);
            int gridDimensions = grid.Cells.Rank;

            // assert
            Assert.AreEqual(2, gridDimensions);
            Assert.AreEqual(Grid.GridSize - 1, numOfRows);
            Assert.AreEqual(Grid.GridSize - 1, numOfColumns);
        }

        [Test]
        public void Cells_Default_AllElementsInitialized()
        {
            // arrange

            // act
            Grid grid = new Grid();

            // assert
            foreach (Cell c in grid.Cells)
                Assert.IsNotNull(c);
        }
        

        // Property: Rows
        [Test]
        public void Rows_Read_IsCorrectLength()
        {
            // arrange

            // act
            int length = _grid.Rows.Count();

            // assert
            Assert.AreEqual(Grid.GridSize, length);
        }

        [Test]
        public void Rows_Default_AllRowsInitialized()
        {
            // arrange

            // act
            Grid grid = new Grid();

            // assert
            foreach (GridRow row in grid.Rows)
                Assert.IsNotNull(row);
        }

        [Test]
        public void Rows_Default_GridRowsAreBuiltAroundMainGrid()
        {
            // arrange

            // act

            // assert
            // Ensure that Grid Rows are constructed around the same grid as exposed in grid.Cells
            // by ensuring that each cell exposed by the Grid Row is findable in grid.Cells
            foreach (var row in _grid.Rows)
                foreach (var cell in row.Cells)
                {
                    Assert.IsNotNull(_grid.Cells.FirstOrDefault(c => c == cell));
                }
        }


        // Property: Columns
        [Test]
        public void Columns_Read_IsCorrectLength()
        {
            // arrange

            // act
            int length = _grid.Columns.Count();

            // assert
            Assert.AreEqual(Grid.GridSize, length);
        }

        [Test]
        public void Columns_Default_AllColumnsInitialized()
        {
            // arrange

            // act
            Grid grid = new Grid();

            // assert
            foreach (GridColumn column in grid.Columns)
                Assert.IsNotNull(column);
        }

        [Test]
        public void Columns_Default_GridColumnsAreBuiltAroundMainGrid()
        {
            // arrange

            // act

            // assert
            // Ensure that Grid Columns are constructed around the same grid as exposed in grid.Cells
            // by ensuring that each cell exposed by the Grid Column is findable in grid.Cells
            foreach (var column in _grid.Columns)
                foreach (var cell in column.Cells)
                {
                    Assert.IsNotNull(_grid.Cells.FirstOrDefault(c => c == cell));
                }
        }


        // Property: Blocks
        [Test]
        public void Blocks_Read_IsCorrectLength()
        {
            // arrange

            // act
            int length = _grid.Blocks.Count();

            // assert
            Assert.AreEqual(Grid.GridSize, length);
        }

        [Test]
        public void Blocks_Default_AllBlocksInitialized()
        {
            // arrange

            // act
            Grid grid = new Grid();

            // assert
            foreach (GridBlock block in grid.Blocks)
                Assert.IsNotNull(block);
        }

        [Test]
        public void Blocks_Default_GridBlocksAreBuiltAroundMainGrid()
        {
            // arrange

            // act

            // assert
            // Ensure that Grid Blocks are constructed around the same grid as exposed in grid.Cells
            // by ensuring that each cell exposed by the Grid Block is findable in grid.Cells
            foreach (var block in _grid.Blocks)
                foreach (var cell in block.Cells)
                {
                    Assert.IsNotNull(_grid.Cells.FirstOrDefault(c => c == cell));
                }
        }


        // Property: Regions
        [Test]
        public void Regions_Read_IsCorrectLength()
        {
            // arrange

            // act
            int length = _grid.Regions.Count();

            // assert
            Assert.AreEqual(Grid.GridSize * 3, length);
        }

        [Test]
        public void Regions_Default_AllRegionsInitialized()
        {
            // arrange

            // act
            Grid grid = new Grid();

            // assert
            foreach (IRegion region in grid.Regions)
                Assert.IsNotNull(region);
        }

        [Test]
        public void Regions_Read_ContainsAllRows()
        {
            // arrange

            // act
            var actualRows = _grid.Regions.Where(x => x.TypeLabel == "Row");

            // assert
            CollectionAssert.AreEqual(actualRows, _grid.Rows);
        }

        [Test]
        public void Regions_Read_ContainsAllColumns()
        {
            // arrange

            // act
            var actualColumns = _grid.Regions.Where(x => x.TypeLabel == "Column");

            // assert
            CollectionAssert.AreEqual(actualColumns, _grid.Columns);
        }

        [Test]
        public void Regions_Read_ContainsAllBlocks()
        {
            // arrange

            // act
            var actualBlocks = _grid.Regions.Where(x => x.TypeLabel == "Block");

            // assert
            CollectionAssert.AreEqual(actualBlocks, _grid.Blocks);
        }


        // Property: AllRegionsAreValid
        [Test]
        public void AllRegionsAreValid_NewGrid_ReturnsTrue()
        {
            // arrange

            // act
            bool allRegionsValid = _grid.AllRegionsAreValid;

            // assert
            Assert.IsTrue(allRegionsValid);
        }

        [Test]
        public void AllRegionsAreValid_FilledValidGrid_ReturnsTrue()
        {
            // arrange
            FillGridNumbers(validFilledGrid);

            // act
            bool allRegionsValid = _grid.AllRegionsAreValid;

            // assert
            Assert.IsTrue(allRegionsValid);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void AllRegionsAreValid_GridWithDuplicateNumbersInRow_ReturnsFalse(int validRowIndex)
        {
            // arrange
            // Ensure row is invalid by installing a duplicate pair
            _grid.Rows[validRowIndex].Cells[0].Number = 2;
            _grid.Rows[validRowIndex].Cells[1].Number = 2;

            // act
            bool allRegionsValid = _grid.AllRegionsAreValid;

            // assert
            Assert.IsFalse(allRegionsValid);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void AllRegionsAreValid_GridWithDuplicateNumbersInColumn_ReturnsFalse(int validColumnIndex)
        {
            // arrange
            // Ensure column is invalid by installing a duplicate pair
            _grid.Columns[validColumnIndex].Cells[0].Number = 8;
            _grid.Columns[validColumnIndex].Cells[1].Number = 8;

            // act
            bool allRegionsValid = _grid.AllRegionsAreValid;

            // assert
            Assert.IsFalse(allRegionsValid);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void AllRegionsAreValid_GridWithDuplicateNumbersInBlock_ReturnsFalse(int validBlockIndex)
        {
            // arrange
            // Ensure block is invalid by installing a duplicate pair
            _grid.Blocks[validBlockIndex].Cells[0].Number = 4;
            _grid.Blocks[validBlockIndex].Cells[1].Number = 4;

            // act
            bool allRegionsValid = _grid.AllRegionsAreValid;

            // assert
            Assert.IsFalse(allRegionsValid);
        }


        // Property: EmptyCells
        [Test]
        public void EmptyCells_NewGrid_Contains81Cells()
        {
            // arrange

            // act
            var actualEmptyCells = _grid.EmptyCells;

            // assert
            Assert.AreEqual(Grid.GridSize * Grid.GridSize, actualEmptyCells.Count());
        }

        [Test]
        public void EmptyCells_NewGrid_ReturnsWholeGrid()
        {
            // arrange

            // act
            var emptyCells = _grid.EmptyCells;

            // assert
            CollectionAssert.AreEqual(_grid.Cells, emptyCells);
        }

        [Test]
        public void EmptyCells_FilledValidGrid_ReturnsEmptyIEnumerable()
        {
            // arrange
            FillGridNumbers(validFilledGrid);

            // act
            var emptyCells = _grid.EmptyCells;

            // assert
            Assert.IsEmpty(emptyCells);
        }

        [Test]
        public void EmptyCells_EvenIndexedRowsFilled_ReturnsCellsInOddIndexedRows()
        {
            // arrange
            FillGridNumbers(validFilledGridEvenIndexRowsFilled);

            var expectedCells = _grid.Cells.Where(c => c.Row.RowIndex % 2 != 0);

            // act
            var emptyCells = _grid.EmptyCells;

            // assert
            CollectionAssert.AreEqual(expectedCells, emptyCells);
        }

        [Test]
        public void EmptyCells_OddIndexedColumnsFilled_ReturnsCellsinEvenIndexedColumns()
        {
            // arrange
            FillGridNumbers(validFilledGridOddIndexColumnsFilled);

            var expectedCells = _grid.Cells.Where(c => c.Column.ColumnIndex % 2 == 0);

            // act
            var emptyCells = _grid.EmptyCells;

            // assert
            CollectionAssert.AreEqual(expectedCells, emptyCells);
        }

        [Test]
        public void EmptyCells_BlocksWithIndexGreaterThanFourFilled_ReturnsCellsInBlocksWithIndexLessThanFive()
        {
            // arrange
            FillGridNumbers(validFilledGridBlockIndexGreaterThanFourFilled);

            var expectedCells = _grid.Cells.Where(c => c.Block.BlockIndex < 5);

            // act
            var emptyCells = _grid.EmptyCells;

            // assert
            CollectionAssert.AreEquivalent(expectedCells, emptyCells);
        }


        // Property: FilledCells
        [Test]
        public void FilledCells_NewGrid_ReturnsEmptyIEnumerable()
        {
            // arrange

            // act
            var filledCells = _grid.FilledCells;

            // assert
            Assert.IsEmpty(filledCells);
        }

        [Test]
        public void FilledCells_FilledValidGrid_ReturnsWholeGrid()
        {
            // arrange
            FillGridNumbers(validFilledGrid);


            // act
            var filledCells = _grid.FilledCells;

            // assert
            CollectionAssert.AreEqual(_grid.Cells, filledCells);
        }

        [Test]
        public void FilledCells_EvenIndexedRowsFilled_ReturnsCellsInEvenIndexedRows()
        {
            // arrange
            FillGridNumbers(validFilledGridEvenIndexRowsFilled);

            var expectedCells = _grid.Cells.Where(c => c.Row.RowIndex % 2 == 0);

            // act
            var filledCells = _grid.FilledCells;

            // assert
            CollectionAssert.AreEqual(expectedCells, filledCells);
        }

        [Test]
        public void FilledCells_OddIndexedColumnsFilled_ReturnsCellsInOddIndexedColumns()
        {
            // arrange
            FillGridNumbers(validFilledGridOddIndexColumnsFilled);

            var expectedCells = _grid.Cells.Where(c => c.Column.ColumnIndex % 2 != 0);

            // act
            var filledCells = _grid.FilledCells;

            // assert
            CollectionAssert.AreEqual(expectedCells, filledCells);
        }

        [Test]
        public void FilledCells_BlocksWithIndexGreaterThanFourFilled_ReturnsCellsInBlockWithIndexGreaterThanFour()
        {
            // arrange
            FillGridNumbers(validFilledGridBlockIndexGreaterThanFourFilled);

            var expectedCells = _grid.Cells.Where(c => c.Block.BlockIndex > 4);

            // act
            var filledCells = _grid.FilledCells;

            // assert
            CollectionAssert.AreEquivalent(expectedCells, filledCells);
        }



        private void FillGridNumbers(int[,] filledGrid)
        {
            for (int rowIndex = 0; rowIndex < Grid.GridSize; rowIndex++)
                for (int columnIndex = 0; columnIndex < Grid.GridSize; columnIndex++)
                {
                    int value = filledGrid[rowIndex, columnIndex];

                    if (value >= 1 && value <= 9)
                        _grid.Cells[rowIndex, columnIndex].Number = value;
                }
        }

        private readonly int[,] validFilledGrid = new int[9, 9]
        {
            { 9, 8, 1,   5, 2, 3,   6, 4, 7 },
            { 6, 3, 4,   8, 7, 9,   2, 5, 1 },
            { 2, 7, 5,   1, 4, 6,   9, 8, 3 },

            { 1, 9, 6,   4, 8, 7,   5, 3, 2 },
            { 5, 4, 8,   3, 1, 2,   7, 6, 9 },
            { 7, 2, 3,   6, 9, 5,   4, 1, 8 },

            { 3, 1, 2,   7, 5, 4,   8, 9, 6 },
            { 4, 6, 9,   2, 3, 8,   1, 7, 5 },
            { 8, 5, 7,   9, 6, 1,   3, 2, 4 }
        };

        private readonly int[,] validFilledGridEvenIndexRowsFilled = new int[9, 9]
        {
            { 9, 8, 1,   5, 2, 3,   6, 4, 7 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 2, 7, 5,   1, 4, 6,   9, 8, 3 },

            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 5, 4, 8,   3, 1, 2,   7, 6, 9 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },

            { 3, 1, 2,   7, 5, 4,   8, 9, 6 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 8, 5, 7,   9, 6, 1,   3, 2, 4 }
        };

        private readonly int[,] validFilledGridOddIndexColumnsFilled = new int[9, 9]
        {
            { 0, 8, 0,   5, 0, 3,   0, 4, 0 },
            { 0, 3, 0,   8, 0, 9,   0, 5, 0 },
            { 0, 7, 0,   1, 0, 6,   0, 8, 0 },

            { 0, 9, 0,   4, 0, 7,   0, 3, 0 },
            { 0, 4, 0,   3, 0, 2,   0, 6, 0 },
            { 0, 2, 0,   6, 0, 5,   0, 1, 0 },

            { 0, 1, 0,   7, 0, 4,   0, 9, 0 },
            { 0, 6, 0,   2, 0, 8,   0, 7, 0 },
            { 0, 5, 0,   9, 0, 1,   0, 2, 0 }
        };

        private readonly int[,] validFilledGridBlockIndexGreaterThanFourFilled = new int[9, 9]
        {
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },
            { 0, 0, 0,   0, 0, 0,   0, 0, 0 },

            { 0, 0, 0,   0, 0, 0,   5, 3, 2 },
            { 0, 0, 0,   0, 0, 0,   7, 6, 9 },
            { 0, 0, 0,   0, 0, 0,   4, 1, 8 },

            { 3, 1, 2,   7, 5, 4,   8, 9, 6 },
            { 4, 6, 9,   2, 3, 8,   1, 7, 5 },
            { 8, 5, 7,   9, 6, 1,   3, 2, 4 }
        };
    }
}
