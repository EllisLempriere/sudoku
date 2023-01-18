using System;
using System.Collections;

namespace Sudoku.Library.SolverSteps
{
    /// <summary>
    /// Step removing candidates precluded by a pre-set cell value
    /// </summary>
    public class RemoveInvalidCandidatesStep : ClearingSolverStep
    {
        /// <summary>
        /// Coordinates of the grid cell containing the pre-set value
        /// </summary>
        public CellCoordinate Location { get; set; }

        /// <summary>
        /// Number value of the pre-set grid cell
        /// </summary>
        public int Number { get; set; }

        public override bool Equals(object obj)
        {
            var step = obj as RemoveInvalidCandidatesStep;

            return step != null &&
                   base.Equals(obj) &&
                   StructuralComparisons.StructuralEqualityComparer.Equals(Location, step.Location) &&
                   Number == step.Number;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Location, Number);
        }
    }
}
