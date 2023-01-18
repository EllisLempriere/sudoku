using System.Collections.Generic;
using System;

namespace Sudoku.Library.Game
{
    public class GridRow : GridRegion, IRegion
    {
        public int RowIndex { get; private set; }

        public string TypeLabel => "Row";

        public int RegionNumber => RowIndex + 1;

        public GridRow(int rowIndex, Cell[,] grid)
        {
            if (rowIndex < 0 || rowIndex >= Grid.GridSize)
                throw new IndexOutOfRangeException($"Invalid row index: {rowIndex}");

            ValidateGrid(grid);

            RowIndex = rowIndex;

            for (int colIndex = 0; colIndex < Grid.GridSize; colIndex++)
            {
                _cells[colIndex] = grid[rowIndex, colIndex];
                _cells[colIndex].Row = this;
            }
            Cells = _cells.AsReadOnly();
        }

        // For testing only, creating dumb GridRow stubs
        internal GridRow()
        {
        }

        public bool ContainsCell(Cell cell)
        {
            return cell.Location.RowIndex == RowIndex;
        }
    }
}
