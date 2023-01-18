using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Library.Game
{
    public static class ArrayExtensions
    {
        public static IReadOnlyArray<T> AsReadOnly<T>(this T[] array)
        {
            return new ReadOnlyArray<T>(array);
        }

        public static IReadOnlyArray<T> AsReadOnly<T>(this T[,] array)
        {
            return new ReadOnlyArray<T>(array);
        }

    }
}
