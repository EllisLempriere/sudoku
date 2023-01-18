using System;
using System.Collections.Generic;
using Sudoku.Library.Game;

namespace Sudoku.Library.Solver
{
    public class RegionCandidateDetails
    {
        public IRegion Region { get; }

        public int CandidateNumber { get; }

        /// <summary>
        /// Number of times the candidate appears in the region
        /// </summary>       
        public int OccurrenceCount { get; }

        /// <summary>
        /// The indexes of the region cells in which the candidate appears
        /// </summary>
        public IReadOnlyList<int> CellIndexes { get; }

        /// <summary>
        /// A value that uniquely identifies the pattern of cell indexes
        /// </summary>
        public int CellIndexPattern { get; }

        public RegionCandidateDetails(IRegion region, int candidateNumber)
        {
            Region = region;
            CandidateNumber = candidateNumber;

            // initialize accumulators
            OccurrenceCount = 0;
            List<int> cellIndexes = new List<int>();
            CellIndexPattern = 0;

            for (int index = 0; index < Grid.GridSize; index++)
            {
                if (region.Cells[index].IsCandidateSet(candidateNumber))
                {
                    OccurrenceCount++;
                    cellIndexes.Add(index);

                    int mask = 1 << index;
                    CellIndexPattern |= mask;
                }
            }

            CellIndexes = cellIndexes;
        }

        public bool BlockCandidateIsLocatedInOneRow()
        {
            uint bits = (uint)CellIndexPattern;

            return (bits & LowerThirdMask) == bits || (bits & MiddleThirdMask) == bits || (bits & UpperThirdMask) == bits;
        }

        public int GetBlockCandidateRowOffset()
        {
            uint bits = (uint)CellIndexPattern;

            if ((bits & LowerThirdMask) == bits)
                return 0;
            else if ((bits & MiddleThirdMask) == bits)
                return 1;
            else if ((bits & UpperThirdMask) == bits)
                return 2;
            else
                throw new InvalidOperationException("Candidates are not located in a row.");
        }

        public bool BlockCandidateIsLocatedInOneColumn()
        {
            uint bits = (uint)CellIndexPattern;

            return (bits & LowerBitCombMask) == bits || (bits & MiddleBitCombMask) == bits || (bits & UpperBitCombMask) == bits;
        }

        public int GetBlockCandidateColumnOffset()
        {
            uint bits = (uint)CellIndexPattern;

            if ((bits & LowerBitCombMask) == bits)
                return 0;
            else if ((bits & MiddleBitCombMask) == bits)
                return 1;
            else if ((bits & UpperBitCombMask) == bits)
                return 2;
            else
                throw new InvalidOperationException("Candidates are not located in a column.");
        }

        public bool RowColumnCandidateIsLocatedInOneBlock()
        {
            uint bits = (uint)CellIndexPattern;

            return (bits & LowerThirdMask) == bits || (bits & MiddleThirdMask) == bits || (bits & UpperThirdMask) == bits;
        }

        public int GetRowColumnCandidateBlockOffset()
        {
            uint bits = (uint)CellIndexPattern;

            if ((bits & LowerThirdMask) == bits)
                return 0;
            else if ((bits & MiddleThirdMask) == bits)
                return 1;
            else if ((bits & UpperThirdMask) == bits)
                return 2;
            else
                throw new InvalidOperationException("Candidates are not located in a row.");
        }

        private const uint LowerThirdMask = 7U;
        private const uint MiddleThirdMask = 7U << 3;
        private const uint UpperThirdMask = 7U << 6;

        private const uint LowerBitCombMask = 73U;
        private const uint MiddleBitCombMask = 73U << 1;
        private const uint UpperBitCombMask = 73U << 2;


    }
}
