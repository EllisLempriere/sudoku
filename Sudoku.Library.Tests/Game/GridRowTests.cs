using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Sudoku.Library.Game;

namespace Sudoku.Library.Tests.Game
{
    [TestFixture]
    public class GridRowTests
    {
        private GridRegionsTestUtils _utilities = new GridRegionsTestUtils();


        // Properties Tests
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
        public void ConstructorIntCellArray_ValidParameters_CreatesInstance(int validRowIndex)
        {
            // arrange
            Cell[,] validGrid = _utilities.CreateValidGrid();

            // act
            GridRow row = new GridRow(validRowIndex, validGrid);

            // assert
            Assert.IsNotNull(row);
        }

        [TestCase(-1)]
        [TestCase(Grid.GridSize)]
        public void ConstructorIntCellArray_InvalidRowIndex_ThrowsException(int invalidRowIndex)
        {
            // arrange
            Cell[,] validGrid = _utilities.CreateValidGrid();

            // act
            TestDelegate act = () => new GridRow(invalidRowIndex, validGrid);

            // assert
            var exception = Assert.Throws<IndexOutOfRangeException>(act);
            Assert.AreEqual($"Invalid row index: {invalidRowIndex}", exception.Message);
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
        public void ConstructorIntCellArray_GridAsNull_ThrowsException(int validRowIndex)
        {
            // arrange

            // act
            TestDelegate act = () => new GridRow(validRowIndex, null);

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
        public void ConstructorIntCellArray_GridOfNullCells_ThrowsException(int validRowIndex)
        {
            // arrange
            Cell[,] gridOfNullCells = new Cell[Grid.GridSize, Grid.GridSize];

            // act
            TestDelegate act = () => new GridRow(validRowIndex, gridOfNullCells);

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
        public void ConstructorIntCellArray_GridWithOneNull_ThrowsException(int validRowIndex)
        {
            // arrange
            Cell[,] gridWithOneNull = _utilities.CreateInvalidGridWithOneNull();

            // act
            TestDelegate act = () => new GridRow(validRowIndex, gridWithOneNull);

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
        public void ConstructorIntCellArray_ContainsDuplicateCells_ThrowsException(int validRowIndex)
        {
            // arrange
            Cell[,] gridWithDuplicateCells = _utilities.CreateInvalidGridWithDuplicateCells();

            // act
            TestDelegate act = () => new GridRow(validRowIndex, gridWithDuplicateCells);

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
        public void ConstructorIntCellArray_GridHasTooFewRows_ThrowsException(int validRowIndex)
        {
            // arrange
            Cell[,] gridWithTooFewRows = new Cell[Grid.GridSize - 1, Grid.GridSize];

            // act
            TestDelegate act = () => new GridRow(validRowIndex, gridWithTooFewRows);
            
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
        public void ConstructorIntCellArray_GridHasTooManyRows_ThrowsException(int validRowIndex)
        {
            // arrange
            Cell[,] gridWithTooManyRows = new Cell[Grid.GridSize + 1, Grid.GridSize];

            // act
            TestDelegate act = () => new GridRow(validRowIndex, gridWithTooManyRows);

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
        public void ConstructorIntCellArray_GridHasTooFewColumns_ThrowsException(int validRowIndex)
        {
            // arrange
            Cell[,] gridWithTooFewColumns = new Cell[Grid.GridSize, Grid.GridSize - 1];

            // act
            TestDelegate act = () => new GridRow(validRowIndex, gridWithTooFewColumns);

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
        public void ConstructorIntCellArray_GridHasTooManyColumns_ThrowsException(int validRowIndex)
        {
            // arrange
            Cell[,] gridWithTooManyColumns = new Cell[Grid.GridSize, Grid.GridSize + 1];

            // act
            TestDelegate act = () => new GridRow(validRowIndex, gridWithTooManyColumns);

            // assert
            var exception = Assert.Throws<ArgumentException>(act);

            string expectedMessage = $"{GridRegion.ErrorInvalidGridWidthMsg} (Parameter '{exception.ParamName}')";
            Assert.AreEqual(expectedMessage, exception.Message);
        }


        // Property: RowIndex
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void RowIndex_Read_ReturnsCorrectValue(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRow(validRowIndex);

            // act
            int readIndex = validRow.RowIndex;

            // assert
            Assert.AreEqual(validRowIndex, readIndex);
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
        public void Cells_Read_IsNotNull(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRow(validRowIndex);

            // act
            var cells = validRow.Cells;

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
        public void Cells_Read_IsCorrectLength(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRow(validRowIndex);

            // act
            var length = validRow.Cells.Length;

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
        public void Cells_Read_CellReferencesAreCorrect(int validRowIndex)
        {
            // arrange
            Cell[,] grid = _utilities.CreateValidGrid();
            GridRow validRow = new GridRow(validRowIndex, grid);

            // act
            var actualCells = validRow.Cells;

            // assert
            Assert.Multiple(() =>
            {
                for (int i = 0; i < Grid.GridSize; i++)
                    Assert.AreSame(grid[validRowIndex, i], actualCells[i], $"cell {i}");
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
        public void IsValid_RowContainsNoNumbers_ReturnsTrue(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRow(validRowIndex);

            // act
            bool valid = validRow.IsValid;

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
        public void IsValid_RowContainsDistinctNumbers_ReturnsTrue(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRowWithDistinctNumbers(validRowIndex);

            // act
            bool valid = validRow.IsValid;

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
        public void IsValid_RowContainsDuplicateNumber_ReturnsFalse(int validRowIndex)
        {
            // arrange
            GridRow invalidRow = _utilities.CreateInvalidGridRowWithDuplicateNumbers(validRowIndex);

            // act
            bool invalid = invalidRow.IsValid;

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
        public void EmptyCells_NoCellsFilled_ReturnsWholeRow(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRow(validRowIndex);

            // act
            var emptyCells = validRow.EmptyCells;

            // assert
            CollectionAssert.AreEqual(emptyCells, validRow.Cells);
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
        public void EmptyCells_EvenIndexCellsFilled_ReturnsOddIndexedCells(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRowWithEvenIndexCellsFilled(validRowIndex);
            Cell[] expectedCells = new Cell[4]
            {
                validRow.Cells[1],
                validRow.Cells[3],
                validRow.Cells[5],
                validRow.Cells[7]
            };

            // act
            var emptyCells = validRow.EmptyCells;

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
        public void EmptyCells_CellsIndexesLessThanFiveFilled_ReturnsCellIndexesGreaterThanFour(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRowWithCellIndexesLessThanFiveFilled(validRowIndex);
            Cell[] expectedCells = new Cell[4]
            {
                validRow.Cells[5],
                validRow.Cells[6],
                validRow.Cells[7],
                validRow.Cells[8]
            };

            // act
            var emptyCells = validRow.EmptyCells;

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
        public void EmptyCells_AllCellsFilled_ReturnsEmptyIEnumerable(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRowWithAllCellsFilled(validRowIndex);

            // act
            var emptyCells = validRow.EmptyCells;

            // assert
            CollectionAssert.IsEmpty(emptyCells);
        }


        // Property: Filled Cells
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void FilledCells_NoCellsFilled_EmptyIEnumerable(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRow(validRowIndex);

            // act
            var filledCells = validRow.FilledCells;

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
        public void FilledCells_EvenIndexCellsFilled_ReturnsEvenIndexedCells(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRowWithEvenIndexCellsFilled(validRowIndex);
            Cell[] expectedCells = new Cell[5]
            {
                validRow.Cells[0],
                validRow.Cells[2],
                validRow.Cells[4],
                validRow.Cells[6],
                validRow.Cells[8]
            };

            // act
            var filledCells = validRow.FilledCells;

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
        public void FilledCells_CellsIndexesLessThanFiveFilled_ReturnsCellIndexesLessThanFive(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRowWithCellIndexesLessThanFiveFilled(validRowIndex);
            Cell[] expectedCells = new Cell[5]
            {
                validRow.Cells[0],
                validRow.Cells[1],
                validRow.Cells[2],
                validRow.Cells[3],
                validRow.Cells[4]
            };

            // act
            var filledCells = validRow.FilledCells;

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
        public void FilledCells_AllCellsFilled_ReturnsWholeRow(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRowWithAllCellsFilled(validRowIndex);

            // act
            var filledCells = validRow.FilledCells;

            // assert
            CollectionAssert.AreEqual(validRow.Cells, filledCells);
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
        public void TypeLabel_Read_ReturnsTextRow(int validRowIndex)
        {
            // arrange
            GridRow validRow = _utilities.CreateValidGridRow(validRowIndex);

            // act
            string label = validRow.TypeLabel;

            // assert
            Assert.AreEqual("Row", label);
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
        public void RegionNumber_Read_ReturnsRowIndexPlusOne(int validRowIndex)
        {
            // arrange
            int expectedNumber = validRowIndex + 1;
            GridRow validRow = _utilities.CreateValidGridRow(validRowIndex);

            // act
            int regionNumber = validRow.RegionNumber;
            
            // assert
            Assert.AreEqual(expectedNumber, regionNumber);
        }
    }
}
