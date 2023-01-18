using System;
using System.Collections;

namespace Sudoku.Library.SolverSteps
{
    public class HiddenSingleStep : ClearingSolverStep
    {
        /// <summary>
        /// Type of the region containing the hidden single
        /// </summary>
        public RegionType PrimaryRegionType { get; set; }

        /// <summary>
        /// Region Number of the region containing the hidden single
        /// </summary>
        public int PrimaryRegionNumber { get; set; }

        /// <summary>
        /// Co-ordinates of the cell containing the hidden single
        /// </summary>
        public CellCoordinate Location { get; set; }

        /// <summary>
        /// Candidate number of focus
        /// </summary>
        public int Candidate { get; set; }

        public override bool Equals(object obj)
        {
            var step = obj as HiddenSingleStep;

            return step != null &&
                   base.Equals(obj) &&
                   PrimaryRegionType == step.PrimaryRegionType &&
                   PrimaryRegionNumber == step.PrimaryRegionNumber &&
                   StructuralComparisons.StructuralEqualityComparer.Equals(Location, step.Location) &&
                   Candidate == step.Candidate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), PrimaryRegionType, PrimaryRegionNumber, Location, Candidate);
        }
    }
}
