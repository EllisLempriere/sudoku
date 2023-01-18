using Sudoku.Library.Game;
using System;

namespace Sudoku.Library.Tests.Game
{
    public class GridRegionsTestUtils
    {
        public GridRegionsTestUtils()
        {
        }

        public Cell[,] CreateValidGrid()
        {
            Cell[,] validGrid = new Cell[Grid.GridSize, Grid.GridSize];

            for (int rowIndex = 0; rowIndex < Grid.GridSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < Grid.GridSize; colIndex++)
                {
                    validGrid[rowIndex, colIndex] = new Cell(rowIndex, colIndex);
                }
            }

            return validGrid;
        }

        public Cell[,] CreateInvalidGridWithOneNull()
        {
            Cell[,] gridWithOneNull = CreateValidGrid();
            int arbitraryRowIndex = 3;
            int arbitraryColumnIndex = 7;

            gridWithOneNull[arbitraryRowIndex, arbitraryColumnIndex] = null;

            return gridWithOneNull;
        }

        public Cell[,] CreateInvalidGridWithDuplicateCells()
        {
            Cell[,] gridWithDuplicateCells = CreateValidGrid();

            (int row, int col) arbitraryCoordinate1 = (2, 6);
            (int row, int col) arbitraryCoordinate2 = (8, 4);

            gridWithDuplicateCells[arbitraryCoordinate1.row, arbitraryCoordinate1.col] = 
                gridWithDuplicateCells[arbitraryCoordinate2.row, arbitraryCoordinate2.col];

            return gridWithDuplicateCells;
        }


        public GridRow CreateValidGridRow(int rowIndex)
        {
            Cell[,] grid = CreateValidGrid();

            return new GridRow(rowIndex, grid);
        }

        public GridRow CreateValidGridRowWithDistinctNumbers(int rowIndex)
        {
            Cell[,] grid = CreateValidGrid();

            grid[rowIndex, 0].Number = 1;
            grid[rowIndex, 1].Number = 2;
            grid[rowIndex, 4].Number = 3;
            grid[rowIndex, 7].Number = 4;
            grid[rowIndex, 8].Number = 5;

            return new GridRow(rowIndex, grid);
        }

        public GridRow CreateInvalidGridRowWithDuplicateNumbers(int rowIndex)
        {
            Cell[,] grid = CreateValidGrid();

            grid[rowIndex, 3].Number = 9;
            grid[rowIndex, 4].Number = 9;
            grid[rowIndex, 5].Number = 9;

            return new GridRow(rowIndex, grid);
        }

        public GridRow CreateValidGridRowWithEvenIndexCellsFilled(int rowIndex)
        {
            Cell[,] grid = CreateValidGrid();

            grid[rowIndex, 0].Number = 1;
            grid[rowIndex, 2].Number = 2;
            grid[rowIndex, 4].Number = 3;
            grid[rowIndex, 6].Number = 4;
            grid[rowIndex, 8].Number = 5;

            return new GridRow(rowIndex, grid);
        }

        public GridRow CreateValidGridRowWithCellIndexesLessThanFiveFilled(int rowIndex)
        {
            Cell[,] grid = CreateValidGrid();

            grid[rowIndex, 0].Number = 5;
            grid[rowIndex, 1].Number = 6;
            grid[rowIndex, 2].Number = 7;
            grid[rowIndex, 3].Number = 8;
            grid[rowIndex, 4].Number = 9;

            return new GridRow(rowIndex, grid);
        }

        public GridRow CreateValidGridRowWithAllCellsFilled(int rowIndex)
        {
            Cell[,] grid = CreateValidGrid();

            for (int i = 0; i < Grid.GridSize; i++)
                grid[rowIndex, i].Number = i + 1;

            return new GridRow(rowIndex, grid);
        }


        public GridColumn CreateValidGridColumn(int columnIndex)
        {
            Cell[,] grid = CreateValidGrid();

            return new GridColumn(columnIndex, grid);
        }

        public GridColumn CreateValidGridColumnWithDistinctNumbers(int columnIndex)
        {
            Cell[,] grid = CreateValidGrid();

            grid[0, columnIndex].Number = 9;
            grid[1, columnIndex].Number = 8;
            grid[4, columnIndex].Number = 7;
            grid[7, columnIndex].Number = 6;
            grid[8, columnIndex].Number = 5;

            return new GridColumn(columnIndex, grid);
        }

        public GridColumn CreateInvalidGridColumnWithDuplicateNumbers(int columnIndex)
        {
            Cell[,] grid = CreateValidGrid();

            grid[3, columnIndex].Number = 1;
            grid[4, columnIndex].Number = 1;
            grid[5, columnIndex].Number = 1;

            return new GridColumn(columnIndex, grid);
        }

        public GridColumn CreateValidGridColumnWithEvenIndexCellsFilled(int columnIndex)
        {
            Cell[,] grid = CreateValidGrid();

            grid[0, columnIndex].Number = 9;
            grid[2, columnIndex].Number = 8;
            grid[4, columnIndex].Number = 7;
            grid[6, columnIndex].Number = 6;
            grid[8, columnIndex].Number = 5;

            return new GridColumn(columnIndex, grid);
        }

        public GridColumn CreateValidGridColumnWithCellIndexesLessThanFiveFilled(int columnIndex)
        {
            Cell[,] grid = CreateValidGrid();

            grid[0, columnIndex].Number = 3;
            grid[1, columnIndex].Number = 4;
            grid[2, columnIndex].Number = 5;
            grid[3, columnIndex].Number = 6;
            grid[4, columnIndex].Number = 7;

            return new GridColumn(columnIndex, grid);
        }        

        public GridColumn CreateValidGridColumnWithAllCellsFilled(int columnIndex)
        {
            Cell[,] grid = CreateValidGrid();

            for (int i = 0; i < Grid.GridSize; i++)
                grid[i, columnIndex].Number = i + 1;

            return new GridColumn(columnIndex, grid);
        }


        public GridBlock CreateValidGridBlock(int blockIndex)
        {
            Cell[,] grid = CreateValidGrid();

            return new GridBlock(blockIndex, grid);
        }

        public GridBlock CreateValidGridBlockWithDistinctNumbers(int blockIndex)
        {
            Cell[,] grid = CreateValidGrid();

            int baseRowIndex = blockIndex / 3 * 3;
            int baseColumnIndex = blockIndex % 3 * 3;

            grid[baseRowIndex, baseColumnIndex].Number = 9;
            grid[baseRowIndex + 1, baseColumnIndex + 1].Number = 8;
            grid[baseRowIndex + 2, baseColumnIndex + 2].Number = 7;
            grid[baseRowIndex + 1, baseColumnIndex].Number = 6;
            grid[baseRowIndex, baseColumnIndex + 1].Number = 5;

            return new GridBlock(blockIndex, grid);
        }

        public GridBlock CreateValidGridBlockWithDuplicateNumbers(int blockIndex)
        {
            Cell[,] grid = CreateValidGrid();

            int baseRowIndex = blockIndex / 3 * 3;
            int baseColumnIndex = blockIndex % 3 * 3;

            grid[baseRowIndex, baseColumnIndex].Number = 4;
            grid[baseRowIndex + 1, baseColumnIndex + 1].Number = 4;
            grid[baseRowIndex + 2, baseColumnIndex + 2].Number = 4;

            return new GridBlock(blockIndex, grid);
        }

        public GridBlock CreateValidGridBlockWithCenterRowFilled(int blockIndex)
        {
            Cell[,] grid = CreateValidGrid();

            int baseRowIndex = blockIndex / 3 * 3;
            int baseColumnIndex = blockIndex % 3 * 3;

            grid[baseRowIndex + 1, baseColumnIndex].Number = 3;
            grid[baseRowIndex + 1, baseColumnIndex + 1].Number = 4;
            grid[baseRowIndex + 1, baseColumnIndex + 2].Number = 5;

            return new GridBlock(blockIndex, grid);
        }

        public GridBlock CreateValidGridBlockWithCellIndexesLessThanFiveFilled(int blockIndex)
        {
            Cell[,] grid = CreateValidGrid();

            int baseRowIndex = blockIndex / 3 * 3;
            int baseColumnIndex = blockIndex % 3 * 3;

            grid[baseRowIndex, baseColumnIndex].Number = 1;
            grid[baseRowIndex, baseColumnIndex + 1].Number = 2;
            grid[baseRowIndex, baseColumnIndex + 2].Number = 3;
            grid[baseRowIndex + 1, baseColumnIndex].Number = 4;
            grid[baseRowIndex + 1, baseColumnIndex + 1].Number = 5;

            return new GridBlock(blockIndex, grid);
        }

        public GridBlock CreateValidGridBlockWithAllCellsFilled(int blockIndex)
        {
            Cell[,] grid = CreateValidGrid();

            int baseRowIndex = blockIndex / 3 * 3;
            int baseColumnIndex = blockIndex % 3 * 3;

            int i = 1;
            for (int rowOffset = 0; rowOffset < Grid.GridSize / 3; rowOffset++)
                for (int columnOffset = 0; columnOffset < Grid.GridSize / 3; columnOffset++)
                {
                    grid[baseRowIndex + rowOffset, baseColumnIndex + columnOffset].Number = i++;
                }

            return new GridBlock(blockIndex, grid);
        }
    }
}
