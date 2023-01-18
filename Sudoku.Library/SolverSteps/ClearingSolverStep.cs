using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Library.SolverSteps
{
    /// <summary>
    /// Ancestor of all SolverSteps that clear candidates
    /// </summary>
    public abstract class ClearingSolverStep : ChangingSolverStep
    {
        /// <summary>
        /// List of candidates cleared from cells as a result of this step
        /// </summary>
        public List<ClearedCandidate> ClearedCandidates { get; } = new List<ClearedCandidate>();

        public override bool Equals(object obj)
        {
            var step = obj as ClearingSolverStep;

            return step != null &&
                   base.Equals(obj) &&
                   ClearedCandidates.SequenceEqual(step.ClearedCandidates);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), ClearedCandidates);
        }
    }
}
