using System;
using System.Collections.Generic;

namespace Sudoku.Library.Game
{
    public class GridRegion
    {
        public IReadOnlyArray<Cell> Cells { get; internal set; }

        public bool IsValid
        {
            get
            {
                var values = new HashSet<int>();

                foreach (Cell c in _cells)
                {
                    if (c.Number.HasValue)
                    {
                        bool added = values.Add(c.Number.Value);

                        if (!added)
                            return false;
                    }
                }

                return true;
            }
        }

        public IEnumerable<Cell> EmptyCells
        {
            get
            {
                foreach (var cell in _cells)
                {
                    if (!cell.Number.HasValue)
                        yield return cell;
                }
            }
        }

        public IEnumerable<Cell> FilledCells
        {
            get
            {
                foreach (var cell in _cells)
                {
                    if (cell.Number.HasValue)
                        yield return cell;
                }
            }
        }
        
        protected void ValidateGrid(Cell[,] grid)
        {
            if (grid == null)
                throw new ArgumentNullException(nameof(grid));

            if (grid.GetUpperBound(0) != Grid.GridSize - 1)
                throw new ArgumentException(ErrorInvalidGridHeightMsg, nameof(grid));

            if (grid.GetUpperBound(1) != Grid.GridSize - 1)
                throw new ArgumentException(ErrorInvalidGridWidthMsg, nameof(grid));

            HashSet<Cell> cells = new HashSet<Cell>();
            foreach (Cell cell in grid)
            {
                if (cell == null)
                    throw new ArgumentException(ErrorInvalidGridWithNullMsg, nameof(grid));

                bool added = cells.Add(cell);
                if (!added)
                    throw new ArgumentException(ErrorInvalidGridWithDuplicateMsg, nameof(grid));
            }
        }

        internal const string ErrorInvalidGridHeightMsg = "Invalid grid size: invalid height";
        internal const string ErrorInvalidGridWidthMsg = "Invalid grid size: invalid width";
        internal const string ErrorInvalidGridWithNullMsg = "Invalid grid: contains a null cell";
        internal const string ErrorInvalidGridWithDuplicateMsg = "Invalid grid: contains a duplicate cell";

        protected readonly Cell[] _cells = new Cell[Grid.GridSize];
    }
}
