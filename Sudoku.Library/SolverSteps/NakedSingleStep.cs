using System;
using System.Collections.Generic;

namespace Sudoku.Library.SolverSteps
{
    public class NakedSingleStep : ClearingSolverStep
    {
        /// <summary>
        /// Co-ordinates of the cell containing the naked single
        /// </summary>
        public CellCoordinate Location { get; set; }

        /// <summary>
        /// Candidate number of focus
        /// </summary>
        public int Candidate { get; set; }

        public override bool Equals(object obj)
        {
            var step = obj as NakedSingleStep;

            return step != null &&
                   base.Equals(obj) &&
                   EqualityComparer<CellCoordinate>.Default.Equals(Location, step.Location) &&
                   Candidate == step.Candidate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Location, Candidate);
        }
    }
}
