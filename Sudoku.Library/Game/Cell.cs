using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Library.Game
{
    public class Cell
    {
        public (int RowIndex, int ColIndex) Location { get; private set; }

        public int? Number
        {
            get => _number;
            set
            {
                if (value.HasValue && (value < 1 || value > 9))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _number = value;
            }
        }

        public IEnumerable<int> Candidates => _candidates;


        public GridRow Row
        {
            get => _gridRow;
            internal set
            {
                _gridRow = value;
                _regions[RowRegionIndex] = value;
            }
        }

        public GridColumn Column
        {
            get => _gridColumn;
            internal set
            {
                _gridColumn = value;
                _regions[ColRegionIndex] = value;
            }
        }

        public GridBlock Block
        {
            get => _gridBlock;
            internal set
            {
                _gridBlock = value;
                _regions[BlockRegionIndex] = value;
            }
        }

        public IEnumerable<IRegion> Regions
        {
            get
            {
                if (_regions.Any(e => e == null))
                {
                    throw new InvalidOperationException("Cell not fully initialized, at least one of Row, Column, Block is not set");
                }
                return _regions;
            }
        }


        public Cell(int rowIndex, int colIndex)
        {
            ValidateRowAndColumnIndexes(rowIndex, colIndex);

            Location = (rowIndex, colIndex);
        }

        public bool IsCandidateSet(int candidate)
        {
            return _candidates.Contains(candidate);
        }

        public bool SetCandidate(int candidate)
        {
            if (candidate < 1 || candidate > 9)
                throw new ArgumentOutOfRangeException(nameof(candidate));

            return _candidates.Add(candidate);
        }

        public bool ClearCandidate(int candidate)
        {
            if (candidate < 1 || candidate > 9)
                throw new ArgumentOutOfRangeException(nameof(candidate));

            return _candidates.Remove(candidate);
        }

        public bool SetCandidates(IEnumerable<int> candidates)
        {
            if (candidates.Any(candidate => candidate < 1 || candidate > 9))
                throw new ArgumentOutOfRangeException(nameof(candidates));

            int initialCount = _candidates.Count;

            _candidates.UnionWith(candidates);

            return initialCount < _candidates.Count;
        }

        public bool ClearCandidates(IEnumerable<int> candidates)
        {
            if (candidates.Any(candidate => candidate < 1 || candidate > 9))
                throw new ArgumentOutOfRangeException(nameof(candidates));

            int initialCount = _candidates.Count;

            _candidates.ExceptWith(candidates);

            return _candidates.Count < initialCount;
        }

        public bool ClearAllCandidates()
        {
            int initialCount = _candidates.Count;

            _candidates.Clear();

            return initialCount > 0;
        }

        private void ValidateRowAndColumnIndexes(int rowIndex, int colIndex)
        {
            if (rowIndex >= Grid.GridSize || rowIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(rowIndex));

            if (colIndex >= Grid.GridSize || colIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(colIndex));
        }

        private int? _number;
        private readonly HashSet<int> _candidates = new HashSet<int>();

        private GridRow _gridRow;
        private GridColumn _gridColumn;
        private GridBlock _gridBlock;

        private readonly IRegion[] _regions = new IRegion[3];
        private const int RowRegionIndex = 0;
        private const int ColRegionIndex = 1;
        private const int BlockRegionIndex = 2;
    }
}

