using System;

namespace Sudoku.Library.SolverSteps
{
    public class ClaimingIntersectionStep : ClearingSolverStep
    {
        /// <summary>
        /// Type of the region constraining the candidate (within the intersecting block)
        /// </summary>
        public RegionType PrimaryRegionType { get; set; }

        /// <summary>
        /// Region Number of the region constraining the candidate (within the intersecting block)
        /// </summary>
        public int PrimaryRegionNumber { get; set; }

        /// <summary>
        /// Other Region Type is always a block for this step type -- property retained for consistency
        /// </summary>
        public RegionType OtherRegionType => RegionType.Block;

        /// <summary>
        /// Block Number of the second region (block) intersecting with the primary region
        /// </summary>
        public int OtherRegionBlockNumber { get; set; }

        /// <summary>
        /// The candidate number of focus
        /// </summary>
        public int Candidate { get; set; }

        public override bool Equals(object obj)
        {
            var step = obj as ClaimingIntersectionStep;

            return step != null &&
                   base.Equals(obj) &&
                   PrimaryRegionType == step.PrimaryRegionType &&
                   PrimaryRegionNumber == step.PrimaryRegionNumber &&
                   OtherRegionType == step.OtherRegionType &&
                   OtherRegionBlockNumber == step.OtherRegionBlockNumber &&
                   Candidate == step.Candidate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), PrimaryRegionType, PrimaryRegionNumber, OtherRegionType, OtherRegionBlockNumber, Candidate);
        }
    }
}
