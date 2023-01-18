using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public class Utilities
    {
        public static CellState[,] CaptureGridState(Grid grid)
        {
            CellState[,] gridState = new CellState[Grid.GridSize, Grid.GridSize];

            for (int rowIndex = 0; rowIndex < Grid.GridSize; rowIndex++)
                for (int colIndex = 0; colIndex < Grid.GridSize; colIndex++)
                {
                    var cell = grid.Cells[rowIndex, colIndex];
                    gridState[rowIndex, colIndex] = new CellState()
                    {
                        Number = cell.Number,
                        Candidates = cell.Candidates.ToArray()
                    };
                }

            return gridState;
        }

        public static CellCoordinate MakeCellCoordinate(Cell cell)
        {
            return new CellCoordinate()
            {
                ColNumber = cell.Location.ColIndex + 1,
                RowNumber = cell.Location.RowIndex + 1
            };
        }


        public static bool ClearCandidatesInFilledCellRegions(Cell filledCell, List<ClearedCandidate> clearedCandidates)
        {
            bool changedGrid = false;

            foreach (var region in filledCell.Regions)
            {
                foreach (var emptyCell in region.EmptyCells)
                {
                    if (emptyCell.ClearCandidate(filledCell.Number.Value))
                    {
                        var info = new ClearedCandidate()
                        {
                            Candidate = filledCell.Number.Value,
                            Location = MakeCellCoordinate(emptyCell),
                            ContextRegionType = MapRegionType(region),
                            ContextRegionNumber = region.RegionNumber
                        };

                        clearedCandidates.Add(info);
                        changedGrid = true;
                    }
                }
            }

            return changedGrid;
        }

        public static RegionType MapRegionType(IRegion region)
        {
            if (region is GridRow)
                return RegionType.Row;
            else if (region is GridColumn)
                return RegionType.Column;
            else if (region is GridBlock)
                return RegionType.Block;
            else
                throw new ArgumentOutOfRangeException(nameof(region));
        }


        /// <summary>
        /// Count the bits set in the specified int
        /// </summary>
        /// <returns>Count of the bits set in the specified integer</returns>
        public int CountSetBits(int value)
        {
            int count = 0;

            uint bitfield = (uint)value;

            while (bitfield > 0)
            {
                if ((bitfield & 1) > 0)
                    count++;

                bitfield = bitfield >> 1;
            }

            return count;
        }

        /// <summary>
        /// Simplistic factorial computation for low integers
        /// </summary>
        public int Factorial(int n)
        {
            if (n < 0 || n > 14)
                throw new ArgumentOutOfRangeException(nameof(n));

            if (n == 0 || n == 1)
                return 1;

            int answer = 1;

            for (int i = n; i > 1; i--)
                answer *= i;

            return answer;
        }

        /// <summary>
        /// Get all unique combinations of 'r' ints chosen from the specified array (or more than r ints)
        /// </summary>
        /// <param name="arr">array of ints to choose from</param>
        /// <param name="r">number of ints to choose for combination</param>
        /// <returns>List of unique combinations</returns>
        public List<HashSet<int>> GetCombinations(int[] arr, int r)
        {
            var combinations = new List<HashSet<int>>();

            // A temporary array to store all combinations one by one 
            int[] data = new int[r];
            int n = arr.Length;

            // Print all combinations using temporary array 'data[]' 
            GenerateCombinations(combinations, arr, r, data, 0, n - 1, 0);

            return combinations;
        }

        /// <summary>
        /// Recursive method to generate combinations chosen from an array
        /// </summary>
        /// <param name="combinations">List in which to accumulate generated combinations</param>
        /// <param name="arr">Input array</param>
        /// <param name="r">Number of items in each combination</param>
        /// <param name="data">Temporary array to store current combination</param>
        /// <param name="arrStartIdx">Staring index in arr[]</param>
        /// <param name="arrEndIdx">Ending index in arr[]</param>
        /// <param name="dataIndex">Current index in data[]</param>
        private void GenerateCombinations(
            List<HashSet<int>> combinations,
            int[] arr,
            int r,
            int[] data,
            int arrStartIdx,
            int arrEndIdx,
            int dataIndex)
        {
            // Current combination is ready to be printed, print it 
            if (dataIndex == r)
            {
                var combo = new HashSet<int>(data);
                combinations.Add(combo);
                return;
            }

            // Previous code used the following as the continuation boolean expression of the for loop
            // (i <= arrEndIdx && arrEndIdx - i + 1 >= r - index)
            // We removed it but aren't sure whether it was necessary.
            // The original code "explained":
            // "The condition 'arrEndIdx-i+1 >= r-index' makes sure that including one element 
            // at index will make a combination with remaining elements at remaining positions.

            for (int i = arrStartIdx; i <= arrEndIdx; i++)
            {
                data[dataIndex] = arr[i]; // pick the current number from array

                GenerateCombinations(
                    combinations: combinations,
                    arr: arr,
                    r: r,
                    data: data,
                    arrStartIdx: i + 1,
                    arrEndIdx: arrEndIdx,
                    dataIndex: dataIndex + 1);
            }
        }
    }
}
