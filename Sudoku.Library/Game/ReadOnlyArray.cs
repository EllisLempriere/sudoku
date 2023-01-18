using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Library.Game
{
    public class ReadOnlyArray<T> : IReadOnlyArray<T>
    {
        public int Length
        {
            get
            {
                if (_array == null)
                    return _twoDArray.Length;
                else
                    return _array.Length;
            }
        }

        public int Rank
        {
            get
            {
                if (_array == null)
                    return _twoDArray.Rank;
                else
                    return _array.Rank;
            }
        }
 
        public T this[int i]
        {
            get
            {
                if (_array == null)
                    throw new InvalidOperationException("Can't use the single dimensional indexer on a two dimensional array");

                return _array[i];
            }
        }

        public T this[int i, int j]
        {
            get
            {
                if (_twoDArray == null)
                    throw new InvalidOperationException("Can't use the two dimensional indexer on a single dimension array");

                return _twoDArray[i, j];
            }
        }

        public ReadOnlyArray(T[] array)
        {
            _array = array ?? throw new ArgumentNullException(nameof(array));
        }

        public ReadOnlyArray(T[,] array)
        {
            _twoDArray = array ?? throw new ArgumentNullException(nameof(array));
        }

        public int GetUpperBound(int dimension)
        {
            if (_array == null)
                return _twoDArray.GetUpperBound(dimension);
            else
                return _array.GetUpperBound(dimension);
        }

        private readonly T[] _array;
        private readonly T[,] _twoDArray;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            if (_array == null)
                return ConvertEnumeratorToGeneric<T>(_twoDArray.GetEnumerator());
            else
                return ((IEnumerable<T>)_array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_array == null)
                return _twoDArray.GetEnumerator();
            else 
                return _array.GetEnumerator();
        }

        private IEnumerator<Q> ConvertEnumeratorToGeneric<Q>(IEnumerator iterator)
        {
            while (iterator.MoveNext())
            {
                yield return (Q)iterator.Current;
            }
        }
    }
}
