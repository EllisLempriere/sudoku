using System;
using System.Collections;
using NUnit.Framework;
using Sudoku.Library.Game;

namespace Sudoku.Library.Tests.Game
{
    [TestFixture]
    public class GridBlockTests
    {
        private GridRegionsTestUtils _utilities = new GridRegionsTestUtils();


        // Property Tests
        // Property: Constructor
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void ConstructorIntCellArray_ValidParameters_CreatesInstance(int validBlockIndex)
        {
            // arrange
            Cell[,] validGrid = _utilities.CreateValidGrid();

            // act
            GridBlock block = new GridBlock(validBlockIndex, validGrid);

            // assert
            Assert.IsNotNull(block);
        }

        [TestCase(-1)]
        [TestCase(Grid.GridSize)]
        public void ConstructorIntCellArray_InvalidBlockIndex_ThrowsException(int invalidBlockIndex)
        {
            // arrange
            Cell[,] validGrid = _utilities.CreateValidGrid();

            // act
            TestDelegate act = () => new GridBlock(invalidBlockIndex, validGrid);

            // assert
            var exception = Assert.Throws<IndexOutOfRangeException>(act);
            Assert.AreEqual($"Invalid block index: {invalidBlockIndex}", exception.Message);
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
        public void ConstructorIntCellArray_GridAsNull_ThrowsException(int validBlockIndex)
        {
            // arrange

            // act
            TestDelegate act = () => new GridBlock(validBlockIndex, null);

            // assert
            Assert.Throws<ArgumentNullException>(act);
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
        public void ConstructorIntCellArray_GridOfNullCells_ThrowsException(int validBlockIndex)
        {
            // arrange
            Cell[,] gridOfNullCells = new Cell[Grid.GridSize, Grid.GridSize];

            // act
            TestDelegate act = () => new GridBlock(validBlockIndex, gridOfNullCells);

            // assert
            var exception = Assert.Throws<ArgumentException>(act);

            string expectedMessage = $"{GridRegion.ErrorInvalidGridWithNullMsg} (Parameter '{exception.ParamName}')";
            Assert.AreEqual(expectedMessage, exception.Message);
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
        public void ConstructorIntCellArray_GridWithOneNull_ThrowsException(int validBlockIndex)
        {
            // arrange
            Cell[,] gridWithOneNull = _utilities.CreateInvalidGridWithOneNull();

            // act
            TestDelegate act = () => new GridBlock(validBlockIndex, gridWithOneNull);

            // assert
            var exception = Assert.Throws<ArgumentException>(act);

            string expectedMessage = $"{GridRegion.ErrorInvalidGridWithNullMsg} (Parameter '{exception.ParamName}')";
            Assert.AreEqual(expectedMessage, exception.Message);
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
        public void ConstructorIntCellArray_ContainsDuplicateCells_ThrowsException(int validBlockIndex)
        {
            // arrange
            Cell[,] gridWithDuplicateCells = _utilities.CreateInvalidGridWithDuplicateCells();

            // act
            TestDelegate act = () => new GridBlock(validBlockIndex, gridWithDuplicateCells);

            // assert
            var exception = Assert.Throws<ArgumentException>(act);

            string expectedMessage = $"{GridRegion.ErrorInvalidGridWithDuplicateMsg} (Parameter '{exception.ParamName}')";
            Assert.AreEqual(expectedMessage, exception.Message);
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
        public void ConstructorIntCellArray_GridHasTooFewRows_ThrowsException(int validBlockIndex)
        {
            // arrange
            Cell[,] gridWithTooFewRows = new Cell[Grid.GridSize - 1, Grid.GridSize];

            // act
            TestDelegate act = () => new GridBlock(validBlockIndex, gridWithTooFewRows);

            // assert
            var exception = Assert.Throws<ArgumentException>(act);

            string expectedMessage = $"{GridRegion.ErrorInvalidGridHeightMsg} (Parameter '{exception.ParamName}')";
            Assert.AreEqual(expectedMessage, exception.Message);
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
        public void ConstructorIntCellArray_GridHasTooManyRows_ThrowsException(int validBlockIndex)
        {
            // arrange
            Cell[,] gridWithTooManyRows = new Cell[Grid.GridSize + 1, Grid.GridSize];

            // act
            TestDelegate act = () => new GridBlock(validBlockIndex, gridWithTooManyRows);

            // assert
            var exception = Assert.Throws<ArgumentException>(act);

            string expectedMessage = $"{GridRegion.ErrorInvalidGridHeightMsg} (Parameter '{exception.ParamName}')";
            Assert.AreEqual(expectedMessage, exception.Message);
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
        public void ConstructorIntCellArray_GridHasTooFewColumns_ThrowsException(int validBlockIndex)
        {
            // arrange
            Cell[,] gridWithTooFewColumns = new Cell[Grid.GridSize, Grid.GridSize - 1];

            // act
            TestDelegate act = () => new GridBlock(validBlockIndex, gridWithTooFewColumns);

            // assert
            var exception = Assert.Throws<ArgumentException>(act);

            string expectedMessage = $"{GridRegion.ErrorInvalidGridWidthMsg} (Parameter '{exception.ParamName}')";
            Assert.AreEqual(expectedMessage, exception.Message);
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
        public void ConstructorIntCellArray_GridHasTooManyColumns_ThrowsException(int validBlockIndex)
        {
            // arrange
            Cell[,] gridWithTooManyColumns = new Cell[Grid.GridSize, Grid.GridSize + 1];

            // act
            TestDelegate act = () => new GridBlock(validBlockIndex, gridWithTooManyColumns);

            // assert
            var exception = Assert.Throws<ArgumentException>(act);

            string expectedMessage = $"{GridRegion.ErrorInvalidGridWidthMsg} (Parameter '{exception.ParamName}')";
            Assert.AreEqual(expectedMessage, exception.Message);
        }


        // Property: BlockIndex
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void BlockIndex_Read_ReturnsCorrectValue(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlock(validBlockIndex);

            // act
            int readIndex = validBlock.BlockIndex;

            // assert
            Assert.AreEqual(validBlockIndex, readIndex);
        }


        // Property: Cells
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void Cells_Read_IsNotNull(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlock (validBlockIndex);

            // act
            var cells = validBlock.Cells;

            // assert
            Assert.IsNotNull(cells);
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
        public void Cells_Read_IsCorrectLength(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlock(validBlockIndex);

            // act
            var length = validBlock.Cells.Length;

            // assert
            Assert.AreEqual(Grid.GridSize, length);
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
        public void Cells_Read_CellReferencesAreCorrect(int validBlockIndex)
        {
            // arrange
            Cell[,] grid = _utilities.CreateValidGrid();
            GridBlock validBlock = new GridBlock(validBlockIndex, grid);

            // act
            var actualCells = validBlock.Cells;

            // assert
            Assert.Multiple(() =>
            {
                int i = 0;
                int baseRowIndex = validBlockIndex / 3 * 3;
                int baseColumnIndex = validBlockIndex % 3 * 3;
                for (int rowOffset = 0; rowOffset < Grid.GridSize / 3; rowOffset++)
                    for (int columnOffset = 0; columnOffset < Grid.GridSize / 3; columnOffset++)
                    {
                        Assert.AreSame(grid[baseRowIndex + rowOffset, baseColumnIndex + columnOffset], actualCells[i], $"cell {i++}");
                    }
            });
        }


        // Property: IsValid
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void IsValid_BlockContainsNoNumbers_ReturnsTrue(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlock(validBlockIndex);

            // act
            bool valid = validBlock.IsValid;

            // assert
            Assert.IsTrue(valid);
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
        public void IsValid_BlockContainsDistinctNumbers_ReturnsTrue(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlockWithDistinctNumbers(validBlockIndex);

            // act
            bool valid = validBlock.IsValid;

            // assert
            Assert.IsTrue(valid);
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
        public void IsValid_BlockContainsDuplicateNumber_ReturnsFalse(int validBlockIndex)
        {
            // arrange
            GridBlock invalidBlock = _utilities.CreateValidGridBlockWithDuplicateNumbers(validBlockIndex);

            // act
            bool invalid = invalidBlock.IsValid;

            // assert
            Assert.IsFalse(invalid);
        }


        // Property: EmptyCells
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void EmptyCells_NoCellsFilled_ReturnsWholeBlock(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlock(validBlockIndex);

            // act
            var emptyCells = validBlock.EmptyCells;

            // assert
            CollectionAssert.AreEqual(emptyCells, validBlock.Cells);
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
        public void EmptyCells_CenterRowOfBlockFilled_ReturnsTopAndBottomCellsOfBlock(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlockWithCenterRowFilled(validBlockIndex);
            Cell[] expectedCells = new Cell[6]
            {
                validBlock.Cells[0],
                validBlock.Cells[1],
                validBlock.Cells[2],
                validBlock.Cells[6],
                validBlock.Cells[7],
                validBlock.Cells[8]
            };

            // act
            var emptyCells = validBlock.EmptyCells;

            // assert
            CollectionAssert.AreEqual(expectedCells, emptyCells);
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
        public void EmptyCells_CellsIndexesLessThanFiveFilled_ReturnsCellIndexesGreaterThanFour(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlockWithCellIndexesLessThanFiveFilled(validBlockIndex);
            Cell[] expectedCells = new Cell[4]
            {
                validBlock.Cells[5],
                validBlock.Cells[6],
                validBlock.Cells[7],
                validBlock.Cells[8]
            };

            // act
            var emptyCells = validBlock.EmptyCells;

            // assert
            CollectionAssert.AreEqual(expectedCells, emptyCells);
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
        public void EmptyCells_AllCellsFilled_ReturnsEmptyIEnumerable(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlockWithAllCellsFilled(validBlockIndex);

            // act
            var emptyCells = validBlock.EmptyCells;

            // assert
            CollectionAssert.IsEmpty(emptyCells);
        }


        // Property: FilledCells
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void FilledCells_NoCellsFilled_EmptyIEnumerable(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlock(validBlockIndex);

            // act
            var filledCells = validBlock.FilledCells;

            // assert
            CollectionAssert.IsEmpty(filledCells);
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
        public void FilledCells_CenterRowOfBlockFilled_ReturnsCenterRowOfBlock(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlockWithCenterRowFilled(validBlockIndex);
            Cell[] expectedCells = new Cell[3]
            {
                validBlock.Cells[3],
                validBlock.Cells[4],
                validBlock.Cells[5]
            };

            // act
            var filledCells = validBlock.FilledCells;

            // assert
            CollectionAssert.AreEqual(expectedCells, filledCells);
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
        public void FilledCells_CellsIndexesLessThanFiveFilled_ReturnsCellIndexesLessThanFive(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlockWithCellIndexesLessThanFiveFilled(validBlockIndex);
            Cell[] expectedCells = new Cell[5]
            {
                validBlock.Cells[0],
                validBlock.Cells[1],
                validBlock.Cells[2],
                validBlock.Cells[3],
                validBlock.Cells[4]
            };

            // act
            var filledCells = validBlock.FilledCells;

            // assert
            CollectionAssert.AreEqual(expectedCells, filledCells);
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
        public void FilledCells_AllCellsFilled_ReturnsWholeBlock(int validBlockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlockWithAllCellsFilled(validBlockIndex);

            // act
            var filledCells = validBlock.FilledCells;

            // assert
            CollectionAssert.AreEqual(validBlock.Cells, filledCells);
        }


        // Property: TypeLabel
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void TypeLabel_Read_ReturnsTextBlock(int blockIndex)
        {
            // arrange
            GridBlock validBlock = _utilities.CreateValidGridBlock(blockIndex);

            // act
            string label = validBlock.TypeLabel;

            // assert
            Assert.AreEqual("Block", label);
        }

        // Property: RegionNumber
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void RegionNumber_Read_ReturnsBlockIndexPlusOne(int validBlockIndex)
        {
            // arrange
            int expectedNumber = validBlockIndex + 1;
            GridBlock validBlock = _utilities.CreateValidGridBlock(validBlockIndex);

            // act
            int regionNumber = validBlock.RegionNumber;

            // assert
            Assert.AreEqual(expectedNumber, regionNumber);
        }
    }
}
