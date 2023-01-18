using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Library.SolverSteps
{
    /// <summary>
    /// Step in which all candidates are assumed/set for all empty cells
    /// </summary>
    public class AssumeCandidatesStep : ChangingSolverStep
    {
        /// <summary>
        /// Coordinates of empty cells for which all candidates were set
        /// </summary>
        public List<CellCoordinate> Locations { get; } = new List<CellCoordinate>();

        public override bool Equals(object obj)
        {
            var step = obj as AssumeCandidatesStep;

            return step != null &&
                   base.Equals(obj) &&
                   Locations.SequenceEqual(step.Locations);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Locations);
        }
    }
}
