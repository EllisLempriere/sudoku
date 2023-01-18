using System;

namespace Sudoku.Library.SolverSteps
{
    public class CellCoordinate
    {
        public int RowNumber { get; set; }
        public int ColNumber { get; set; }

        public override bool Equals(object obj)
        {
            var coordinate = obj as CellCoordinate;

            return coordinate != null &&
                   RowNumber == coordinate.RowNumber &&
                   ColNumber == coordinate.ColNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowNumber, ColNumber);
        }
    }
}
