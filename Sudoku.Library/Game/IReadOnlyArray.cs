using System.Collections.Generic;

namespace Sudoku.Library.Game
{
    public interface IReadOnlyArray<out T> : IEnumerable<T>
    {
        int Length { get; }
        int Rank { get; }

        T this[int index] { get; }
        T this[int i, int j] { get; }

        int GetUpperBound(int dimension);
    }
}
