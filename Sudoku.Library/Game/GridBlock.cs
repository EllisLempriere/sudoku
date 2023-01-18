using System.Collections.Generic;
using System;

namespace Sudoku.Library.Game
{
    public class GridBlock : GridRegion, IRegion
    {
        public int BlockIndex { get; private set; }

        public string TypeLabel => "Block";

        public int RegionNumber => BlockIndex + 1;

        public GridBlock(int blockIndex, Cell[,] grid)
        {
            if (blockIndex < 0 || blockIndex >= Grid.GridSize)
                throw new IndexOutOfRangeException($"Invalid block index: {blockIndex}");

            ValidateGrid(grid);

            BlockIndex = blockIndex;

            var baseRowIndex = blockIndex / 3 * 3;
            var baseColumnIndex = blockIndex % 3 * 3;

            for (int rowOffset = 0; rowOffset < Grid.GridSize / 3; rowOffset++)
                for (int colOffset = 0; colOffset < Grid.GridSize / 3; colOffset++)
                {
                    int cellIndex = rowOffset * 3 + colOffset;
                    _cells[cellIndex] = grid[baseRowIndex + rowOffset, baseColumnIndex + colOffset];
                    _cells[cellIndex].Block = this;
                }
            Cells = _cells.AsReadOnly();
        }

        // For testing only, creating dumb GridBlock stubs
        internal GridBlock()
        {
        }

        public bool ContainsCell(Cell cell)
        {
            var baseRowIndex = BlockIndex / 3 * 3;
            var rowOffset = cell.Location.RowIndex - baseRowIndex;

            var baseColIndex = BlockIndex % 3 * 3;
            var colOffset = cell.Location.ColIndex - baseColIndex;

            return rowOffset >= 0 && rowOffset <= 2 && colOffset >= 0 && colOffset <= 2;
        }
    }
}
