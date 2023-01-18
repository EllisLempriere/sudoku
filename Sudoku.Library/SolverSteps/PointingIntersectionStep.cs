using System;

namespace Sudoku.Library.SolverSteps
{
    public class PointingIntersectionStep : ClearingSolverStep
    {
        /// <summary>
        /// Primary Region Type is always a block for this step type -- property retained for consistency
        /// </summary>
        public RegionType PrimaryRegionType => RegionType.Block;

        /// <summary>
        /// Block number of the block constraining the candidate (within the intersection with the other region)
        /// </summary>
        public int PrimaryRegionBlockNumber { get; set; }

        /// <summary>
        /// Type of the second region that intersects with the block
        /// </summary>
        public RegionType OtherRegionType { get; set; }

        /// <summary>
        /// Region Number of the second region that intersects with the block
        /// </summary>
        public int OtherRegionNumber { get; set; }

        /// <summary>
        /// The candidate number of focus
        /// </summary>
        public int Candidate { get; set; }

        public override bool Equals(object obj)
        {
            var step = obj as PointingIntersectionStep;

            return step != null &&
                   base.Equals(obj) &&
                   PrimaryRegionType == step.PrimaryRegionType &&
                   PrimaryRegionBlockNumber == step.PrimaryRegionBlockNumber &&
                   OtherRegionType == step.OtherRegionType &&
                   OtherRegionNumber == step.OtherRegionNumber &&
                   Candidate == step.Candidate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                base.GetHashCode(),
                PrimaryRegionType,
                PrimaryRegionBlockNumber,
                OtherRegionType,
                OtherRegionNumber,
                Candidate);
        }
    }
}
