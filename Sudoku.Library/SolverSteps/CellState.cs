using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Library.SolverSteps
{
    public class CellState
    {
        public int? Number { get; set; }
        public int[] Candidates { get; set; }

        public override bool Equals(object obj)
        {
            var state = obj as CellState;

            return state != null &&
                   EqualityComparer<int?>.Default.Equals(Number, state.Number) &&
                   Candidates.SequenceEqual(state.Candidates);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, Candidates);
        }
    }
}
