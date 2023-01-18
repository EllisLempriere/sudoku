using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Library.Game
{
    public class Grid
    {
        public const int GridSize = 9;

        public IReadOnlyArray<Cell> Cells { get; private set; }

        public IReadOnlyArray<GridRow> Rows { get; private set; }
        public IReadOnlyArray<GridColumn> Columns { get; private set; }
        public IReadOnlyArray<GridBlock> Blocks  { get; private set; }
        public IEnumerable<IRegion> Regions => _gridRegions;

        public bool AllRegionsAreValid => Regions.All(r => r.IsValid);

        public IEnumerable<Cell> EmptyCells
        {
            get
            {
                foreach (var cell in _gridCells)
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
                foreach (var cell in _gridCells)
                {
                    if (cell.Number.HasValue)
                        yield return cell;
                }
            }
        }

        public Grid()
        {
            // init grid of Cells
            for (int rowIndex = 0; rowIndex < GridSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < GridSize; colIndex++)
                {
                    _gridCells[rowIndex, colIndex] = new Cell(rowIndex, colIndex);
                }
            }
            Cells = _gridCells.AsReadOnly();

            int regionIndex = 0;

            // init array of Rows based on grid; update its cells
            for (int rowIndex = 0; rowIndex < GridSize; rowIndex++)
            {
                _gridRows[rowIndex] = new GridRow(rowIndex, _gridCells);
                _gridRegions[regionIndex++] = _gridRows[rowIndex];
            }
            Rows = _gridRows.AsReadOnly();

            // init array of Columns based on grid; update its cells
            for (int colIndex = 0; colIndex < GridSize; colIndex++)
            {
                _gridColumns[colIndex] = new GridColumn(colIndex, _gridCells);
                _gridRegions[regionIndex++] = _gridColumns[colIndex];
            }
            Columns = _gridColumns.AsReadOnly();

            // init array of Blocks based on grid; update its cells
            for (int blockIndex = 0; blockIndex < GridSize; blockIndex++)
            {
                _gridBlocks[blockIndex] = new GridBlock(blockIndex, _gridCells);
                _gridRegions[regionIndex++] = _gridBlocks[blockIndex];
            }
            Blocks = _gridBlocks.AsReadOnly();
        }

        private readonly Cell[,] _gridCells = new Cell[GridSize, GridSize];
        private readonly GridRow[] _gridRows = new GridRow[GridSize];
        private readonly GridColumn[] _gridColumns = new GridColumn[GridSize];
        private readonly GridBlock[] _gridBlocks = new GridBlock[GridSize];
        private readonly IRegion[] _gridRegions = new IRegion[GridSize * 3];
    }
}
