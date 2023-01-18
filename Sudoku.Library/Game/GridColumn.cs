using System;
using System.Collections.Generic;

namespace Sudoku.Library.Game
{
    public class GridColumn : GridRegion, IRegion
    {
        public int ColumnIndex { get; private set; }

        public string TypeLabel => "Column";

        public int RegionNumber => ColumnIndex + 1;

        public GridColumn(int colIndex, Cell[,] grid)
        {
            if (colIndex < 0 || colIndex >= Grid.GridSize)
                throw new IndexOutOfRangeException($"Invalid column index: {colIndex}");

            ValidateGrid(grid);

            ColumnIndex = colIndex;

            for (int rowIndex = 0; rowIndex < Grid.GridSize; rowIndex++)
            {
                _cells[rowIndex] = grid[rowIndex, colIndex];
                _cells[rowIndex].Column = this;
            }
            Cells = _cells.AsReadOnly();
        }

        // For testing only, creating dumb GridColumn stubs
        internal GridColumn()
        {
        }

        public bool ContainsCell(Cell cell)
        {
            return cell.Location.ColIndex == ColumnIndex;
        }
    }
}
