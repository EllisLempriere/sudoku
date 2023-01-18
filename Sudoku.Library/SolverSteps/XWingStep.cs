using System;
using System.Linq;

namespace Sudoku.Library.SolverSteps
{
    public class XWingStep : ClearingSolverStep
    {
        /// <summary>
        /// Type of the region constraining the candidate
        /// </summary>
        public RegionType PrimaryRegionType
        {
            get => _primaryRegionType;
            set
            {
                if (value != RegionType.Row && value != RegionType.Column)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _primaryRegionType = value;

                if (value == RegionType.Row)
                    OtherRegionType = RegionType.Column;
                else
                    OtherRegionType = RegionType.Row;
            }
        }

        /// <summary>
        /// The two Region Numbers of the primary regions forming the X-Wing
        /// </summary>
        public int[] PrimaryRegionNumbers
        {
            get => _primaryRegionNumbers;
            set
            {
                if (value.Rank != 1 || value.Length != 2)
                    throw new ArgumentOutOfRangeException(nameof(value), "Must be single-dimensional array of length 2");

                _primaryRegionNumbers = value;
            }
        }

        /// <summary>
        /// Type of the region intersecting the primary regions, forming the X-Wing
        /// </summary>
        public RegionType OtherRegionType { get; private set; }

        /// <summary>
        /// The two Region Numbers of the regions intersecting the primary regions, forming the X-Wing
        /// </summary>
        public int[] OtherRegionNumbers
        {
            get => _otherRegionNumbers;
            set
            {
                if (value.Rank != 1 || value.Length != 2)
                    throw new ArgumentOutOfRangeException(nameof(value), "Must be single-dimensional array of length 2");

                _otherRegionNumbers = value;
            }
        }

        /// <summary>
        /// The candidate number of focus
        /// </summary>
        public int Candidate { get; set; }


        public override bool Equals(object obj)
        {
            var step = obj as XWingStep;

            return step != null &&
                   base.Equals(obj) &&
                   PrimaryRegionType == step.PrimaryRegionType &&
                   PrimaryRegionNumbers.SequenceEqual(step.PrimaryRegionNumbers) &&
                   OtherRegionType == step.OtherRegionType &&
                   OtherRegionNumbers.SequenceEqual(step.OtherRegionNumbers) &&
                   Candidate == step.Candidate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                base.GetHashCode(),
                PrimaryRegionType,
                PrimaryRegionNumbers,
                OtherRegionType,
                OtherRegionNumbers,
                Candidate);
        }

        private RegionType _primaryRegionType = RegionType.Row;
        private int[] _primaryRegionNumbers = new int[2];
        private int[] _otherRegionNumbers = new int[2];
    }
}
