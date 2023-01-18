using System;
using System.Linq;

namespace Sudoku.Library.SolverSteps
{
    public class HiddenDoubleStep : ClearingSolverStep
    {
        /// <summary>
        /// Type of the region constraining the hidden double
        /// </summary>
        public RegionType PrimaryRegionType { get; set; }

        /// <summary>
        /// Region Number of the region constraining the hidden double
        /// </summary>
        public int PrimaryRegionNumber { get; set; }

        /// <summary>
        /// Co-ordinates of the two region cells containing the hidden double
        /// </summary>
        public CellCoordinate[] Locations
        {
            get => _locations;
            set
            {
                if (value.Rank != 1 || value.Length != 2)
                    throw new ArgumentOutOfRangeException(nameof(value), "Must be single-dimensional array of length 2");

                _locations = value;
            }
        }

        /// <summary>
        /// The two candidate numbers of focus
        /// </summary>
        public int[] Candidates
        {
            get => _candidates;
            set
            {
                if (value.Rank != 1 || value.Length != 2)
                    throw new ArgumentOutOfRangeException(nameof(value), "Must be single-dimensional array of length 2");

                _candidates = value;
            }
        }

        public override bool Equals(object obj)
        {
            var step = obj as HiddenDoubleStep;

            return step != null &&
                   base.Equals(obj) &&
                   PrimaryRegionType == step.PrimaryRegionType &&
                   PrimaryRegionNumber == step.PrimaryRegionNumber &&
                   Locations.SequenceEqual(step.Locations) &&
                   Candidates.SequenceEqual(step.Candidates);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), PrimaryRegionType, PrimaryRegionNumber, Locations, Candidates);
        }

        private CellCoordinate[] _locations = new CellCoordinate[2];
        private int[] _candidates = new int[2];
    }
}
