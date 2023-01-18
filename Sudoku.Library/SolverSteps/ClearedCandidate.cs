using System;
using System.Collections.Generic;

namespace Sudoku.Library.SolverSteps
{
    public class ClearedCandidate
    {
        /// <summary>
        /// Candidate number that was cleared
        /// </summary>
        public int Candidate { get; set; }

        /// <summary>
        /// Coordinates of the cell from which the candidate was cleared
        /// </summary>
        public CellCoordinate Location { get; set; }

        /// <summary>
        /// Type of the region (related to the cell) most involved in the step reasoning
        /// </summary>
        public RegionType ContextRegionType { get; set; }

        /// <summary>
        /// Region Number of the region (related to the cell) most involved in the step reasoning
        /// </summary>
        public int ContextRegionNumber { get; set; }

        public override bool Equals(object obj)
        {
            var candidate = obj as ClearedCandidate;

            return candidate != null &&
                   Candidate == candidate.Candidate &&
                   EqualityComparer<CellCoordinate>.Default.Equals(Location, candidate.Location) &&
                   ContextRegionType == candidate.ContextRegionType &&
                   ContextRegionNumber == candidate.ContextRegionNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Candidate, Location, ContextRegionType, ContextRegionNumber);
        }
    }
}
