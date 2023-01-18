using System;
using System.Collections;
using NUnit.Framework;
using Sudoku.Library.Game;

namespace Sudoku.Library.Tests.Game
{
    [TestFixture]
    public class GridColumnTests
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
        public void ConstructorIntCellArray_ValidParameters_CreatesInstance(int validColumnIndex)
        {
            // arrange
            Cell[,] validGrid = _utilities.CreateValidGrid();

            // act
            GridColumn column = new GridColumn(validColumnIndex, validGrid);

            // assert
            Assert.IsNotNull(column);
        }

        [TestCase(-1)]
        [TestCase(Grid.GridSize)]
        public void ConstructorIntCellArray_InvalidColumnIndex_ThrowsException(int invalidColumnIndex)
        {
            // arrange
            Cell[,] validGrid = _utilities.CreateValidGrid();

            // act
            TestDelegate act = () => new GridColumn(invalidColumnIndex, validGrid);

            // assert
            var exception = Assert.Throws<IndexOutOfRangeException>(act);
            Assert.AreEqual($"Invalid column index: {invalidColumnIndex}", exception.Message);
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
        public void ConstructorIntCellArray_GridAsNull_ThrowsException(int validColumnIndex)
        {
            // arrange

            // act
            TestDelegate act = () => new GridColumn(validColumnIndex, null);

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
        public void ConstructorIntCellArray_GridOfNullCells_ThrowsException(int validColumnIndex)
        {
            // arrange
            Cell[,] gridOfNullCells = new Cell[Grid.GridSize, Grid.GridSize];

            // act
            TestDelegate act = () => new GridColumn(validColumnIndex, gridOfNullCells);

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
        public void ConstructorIntCellArray_GridWithOneNull_ThrowsException(int validColumnIndex)
        {
            // arrange
            Cell[,] gridWithOneNull = _utilities.CreateInvalidGridWithOneNull();

            // act
            TestDelegate act = () => new GridColumn(validColumnIndex, gridWithOneNull);

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
        public void ConstructorIntCellArray_ContainsDuplicateCells_ThrowsException(int validColumnIndex)
        {
            // arrange
            Cell[,] gridWithDuplicateCells = _utilities.CreateInvalidGridWithDuplicateCells();

            // act
            TestDelegate act = () => new GridColumn(validColumnIndex, gridWithDuplicateCells);

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
        public void ConstructorIntCellArray_GridHasTooFewRows_ThrowsException(int validColumnIndex)
        {
            // arrange
            Cell[,] gridWithTooFewRows = new Cell[Grid.GridSize - 1, Grid.GridSize];

            // act
            TestDelegate act = () => new GridColumn(validColumnIndex, gridWithTooFewRows);

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
        public void ConstructorIntCellArray_GridHasTooManyRows_ThrowsException(int validColumnIndex)
        {
            // arrange
            Cell[,] gridWithTooManyRows = new Cell[Grid.GridSize + 1, Grid.GridSize];

            // act
            TestDelegate act = () => new GridColumn(validColumnIndex, gridWithTooManyRows);

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
        public void ConstructorIntCellArray_GridHasTooFewColumns_ThrowsException(int validColumnIndex)
        {
            // arrange
            Cell[,] gridWithTooFewColumns = new Cell[Grid.GridSize, Grid.GridSize - 1];

            // act
            TestDelegate act = () => new GridColumn(validColumnIndex, gridWithTooFewColumns);

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
        public void ConstructorIntCellArray_GridHasTooManyColumns_ThrowsException(int validColumnIndex)
        {
            // arrange
            Cell[,] gridWithTooManyColumns = new Cell[Grid.GridSize, Grid.GridSize + 1];

            // act
            TestDelegate act = () => new GridColumn(validColumnIndex, gridWithTooManyColumns);

            // assert
            var exception = Assert.Throws<ArgumentException>(act);

            string expectedMessage = $"{GridRegion.ErrorInvalidGridWidthMsg} (Parameter '{exception.ParamName}')";
            Assert.AreEqual(expectedMessage, exception.Message);
        }


        // Property: ColumnIndex
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void ColumnIndex_Read_ReturnsCorrectValue(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumn(validColumnIndex);

            // act
            int readIndex = validColumn.ColumnIndex;

            // assert
            Assert.AreEqual(validColumnIndex, readIndex);
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
        public void Cells_Read_IsNotNull(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumn(validColumnIndex);

            // act
            var cells = validColumn.Cells;

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
        public void Cells_Read_IsCorrectLength(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumn(validColumnIndex);

            // act
            var length = validColumn.Cells.Length;

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
        public void Cells_Read_CellReferencesAreCorrect(int validColumnIndex)
        {
            // arrange
            Cell[,] grid = _utilities.CreateValidGrid();
            GridColumn validColumn = new GridColumn(validColumnIndex, grid);

            // act
            var actualCells = validColumn.Cells;

            // assert
            Assert.Multiple(() =>
            {
                for (int i = 0; i < Grid.GridSize; i++)
                    Assert.AreSame(grid[i, validColumnIndex], actualCells[i], $"cell {i}");
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
        public void IsValid_ColumnContainsNoNumbers_ReturnsTrue(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumn(validColumnIndex);

            // act
            bool valid = validColumn.IsValid;

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
        public void IsValid_ColumnContainsDistinctNumbers_ReturnsTrue(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumnWithDistinctNumbers(validColumnIndex);

            // act
            bool valid = validColumn.IsValid;

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
        public void IsValid_ColumnContainsDuplicateNumber_ReturnsFalse(int validColumnIndex)
        {
            // arrange
            GridColumn invalidColumn = _utilities.CreateInvalidGridColumnWithDuplicateNumbers(validColumnIndex);

            // act
            bool invalid = invalidColumn.IsValid;

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
        public void EmptyCells_NoCellsFilled_ReturnsWholeColumn(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumn(validColumnIndex);

            // act
            var emptyCells = validColumn.EmptyCells;

            // assert
            CollectionAssert.AreEqual(emptyCells, validColumn.Cells);
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
        public void EmptyCells_EvenIndexCellsFilled_ReturnsOddIndexedCells(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumnWithEvenIndexCellsFilled(validColumnIndex);
            Cell[] expectedCells = new Cell[4]
            {
                validColumn.Cells[1],
                validColumn.Cells[3],
                validColumn.Cells[5],
                validColumn.Cells[7]
            };

            // act
            var emptyCells = validColumn.EmptyCells;

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
        public void EmptyCells_CellsIndexesLessThanFiveFilled_ReturnsCellIndexesGreaterThanFour(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumnWithCellIndexesLessThanFiveFilled(validColumnIndex);
            Cell[] expectedCells = new Cell[4]
            {
                validColumn.Cells[5],
                validColumn.Cells[6],
                validColumn.Cells[7],
                validColumn.Cells[8]
            };

            // act
            var emptyCells = validColumn.EmptyCells;

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
        public void EmptyCells_AllCellsFilled_ReturnsEmptyIEnumerable(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumnWithAllCellsFilled(validColumnIndex);

            // act
            var emptyCells = validColumn.EmptyCells;

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
        public void FilledCells_NoCellsFilled_EmptyIEnumerable(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumn(validColumnIndex);

            // act
            var filledCells = validColumn.FilledCells;

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
        public void FilledCells_EvenIndexCellsFilled_ReturnsEvenIndexedCells(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumnWithEvenIndexCellsFilled(validColumnIndex);
            Cell[] expectedCells = new Cell[5]
            {
                validColumn.Cells[0],
                validColumn.Cells[2],
                validColumn.Cells[4],
                validColumn.Cells[6],
                validColumn.Cells[8]
            };

            // act
            var filledCells = validColumn.FilledCells;

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
        public void FilledCells_CellsIndexesLessThanFiveFilled_ReturnsCellIndexesLessThanFive(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumnWithCellIndexesLessThanFiveFilled(validColumnIndex);
            Cell[] expectedCells = new Cell[5]
            {
                validColumn.Cells[0],
                validColumn.Cells[1],
                validColumn.Cells[2],
                validColumn.Cells[3],
                validColumn.Cells[4]
            };

            // act
            var filledCells = validColumn.FilledCells;

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
        public void FilledCells_AllCellsFilled_ReturnsWholeColumn(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumnWithAllCellsFilled(validColumnIndex);

            // act
            var filledCells = validColumn.FilledCells;

            // assert
            CollectionAssert.AreEqual(validColumn.Cells, filledCells);
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
        public void TypeLabel_Read_ReturnsTextColumn(int validColumnIndex)
        {
            // arrange
            GridColumn validColumn = _utilities.CreateValidGridColumn(validColumnIndex);

            // act
            string label = validColumn.TypeLabel;

            // assert
            Assert.AreEqual("Column", label);
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
        public void RegionNumber_Read_ReturnsColumnIndexPlusOne(int validColumnIndex)
        {
            // arrange
            int expectedNumber = validColumnIndex + 1;
            GridColumn validColumn = _utilities.CreateValidGridColumn(validColumnIndex);

            // act
            int regionNumber = validColumn.RegionNumber;
            
            // assert
            Assert.AreEqual(expectedNumber, regionNumber);
        }
    }
}
