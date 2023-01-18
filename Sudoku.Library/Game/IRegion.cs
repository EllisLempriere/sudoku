using System.Collections.Generic;

namespace Sudoku.Library.Game
{
    public interface IRegion
    {
        bool IsValid { get; }

        IReadOnlyArray<Cell> Cells { get; }
        IEnumerable<Cell> EmptyCells { get; }
        IEnumerable<Cell> FilledCells { get; }

        string TypeLabel { get; }
        int RegionNumber { get; }

        bool ContainsCell(Cell cell);
    }
}
