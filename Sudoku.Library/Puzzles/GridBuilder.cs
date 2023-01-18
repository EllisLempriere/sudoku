using Sudoku.Library.Game;

namespace Sudoku.Library.Puzzles
{
    public class GridBuilder
    {
        public Grid Build(PuzzleId p)
        {
            int[,] cells = GetCells(p);

            Grid g = new Grid();

            for (int rowIndex = 0; rowIndex < Grid.GridSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < Grid.GridSize; colIndex++)
                {
                    int value = cells[rowIndex, colIndex];

                    if (value >= 1 && value <= 9)
                        g.Cells[rowIndex, colIndex].Number = value;
                }
            }

            return g;
        }

        private int[,] GetCells(PuzzleId p)
        {
            switch (p)
            {
                case PuzzleId.Easy03:
                default:
                    return _easy03;

                case PuzzleId.Moderate02:
                    return _moderate02;

                case PuzzleId.Challenging62:
                    return _challenging62;

                case PuzzleId.Challenging63:
                    return _challenging63;

                case PuzzleId.Tricky750:
                    return _tricky750;
            }
        }


        private readonly int[,] _easy03 = new int[9, 9]
        {
            { 0, 6, 3,   1, 0, 8,   9, 5, 0},
            { 1, 0, 0,   5, 0, 2,   0, 0, 6},
            { 7, 0, 0,   0, 0, 0,   0, 0, 2},

            { 8, 2, 0,   0, 6, 0,   0, 4, 9},
            { 0, 0, 0,   9, 0, 5,   0, 0, 0},
            { 9, 7, 0,   0, 8, 0,   0, 2, 5},

            { 5, 0, 0,   0, 0, 0,   0, 0, 4},
            { 3, 0, 0,   2, 0, 7,   0, 0, 8},
            { 0, 9, 2,   8, 0, 4,   1, 7, 0}
        };

        private readonly int[,] _moderate02 = new int[9, 9]
        {
            { 2, 0, 9,   0, 0, 7,   0, 0, 0},
            { 8, 0, 1,   2, 0, 0,   0, 6, 0},
            { 0, 0, 0,   0, 1, 0,   0, 0, 2},

            { 3, 9, 0,   0, 0, 5,   0, 1, 0},
            { 6, 2, 0,   0, 0, 0,   0, 9, 8},
            { 0, 4, 0,   8, 0, 0,   0, 3, 5},

            { 4, 0, 0,   0, 9, 0,   0, 0, 0},
            { 0, 8, 0,   0, 0, 4,   6, 0, 1},
            { 0, 0, 0,   1, 0, 0,   9, 0, 4}
        };

        private readonly int[,] _challenging62 = new int[9, 9]
        {
            { 5, 2, 0,   0, 6, 0,   0, 7, 0},
            { 0, 0, 0,   2, 4, 1,   0, 0, 0},
            { 0, 0, 4,   0, 7, 0,   0, 0, 0},

            { 2, 0, 0,   5, 0, 0,   0, 8, 9},
            { 0, 8, 0,   0, 0, 0,   0, 5, 0},
            { 7, 1, 0,   0, 0, 3,   0, 0, 4},

            { 0, 0, 0,   0, 8, 0,   2, 0, 0},
            { 0, 0, 0,   1, 5, 4,   0, 0, 0},
            { 0, 6, 0,   0, 3, 0,   0, 1, 5}
        };

        private readonly int[,] _challenging63 = new int[9, 9]
        {
            { 0, 9, 0,   0, 6, 0,   0, 4, 0},
            { 8, 0, 0,   4, 0, 2,   0, 0, 9},
            { 0, 0, 7,   0, 0, 0,   1, 0, 0},

            { 0, 6, 0,   0, 2, 0,   0, 7, 0},
            { 1, 0, 0,   6, 0, 7,   0, 0, 5},
            { 0, 2, 0,   0, 1, 0,   0, 8, 0},

            { 0, 0, 8,   0, 0, 0,   3, 0, 0},
            { 3, 0, 0,   9, 0, 6,   0, 0, 7},
            { 0, 5, 0,   0, 4, 0,   0, 1, 0}
        };

        private readonly int[,] _tricky750 = new int[9, 9]
        {
            { 4, 0, 7,   0, 2, 0,   0, 6, 1},
            { 0, 8, 2,   0, 6, 0,   0, 4, 0},
            { 0, 0, 0,   0, 0, 0,   0, 0, 0},

            { 2, 4, 0,   9, 0, 0,   0, 0, 7},
            { 1, 0, 0,   2, 0, 3,   0, 0, 9},
            { 7, 0, 0,   0, 0, 6,   0, 1, 2},

            { 0, 0, 0,   0, 0, 0,   0, 0, 0},
            { 0, 2, 0,   0, 3, 0,   5, 7, 0},
            { 3, 6, 0,   0, 4, 0,   1, 0, 8}
        };

#pragma warning disable S1144 // Unused private types or members should be removed
        private readonly int[,] _template = new int[9, 9]
        {
            { 0, 0, 0,   0, 0, 0,   0, 0, 0},
            { 0, 0, 0,   0, 0, 0,   0, 0, 0},
            { 0, 0, 0,   0, 0, 0,   0, 0, 0},

            { 0, 0, 0,   0, 0, 0,   0, 0, 0},
            { 0, 0, 0,   0, 0, 0,   0, 0, 0},
            { 0, 0, 0,   0, 0, 0,   0, 0, 0},

            { 0, 0, 0,   0, 0, 0,   0, 0, 0},
            { 0, 0, 0,   0, 0, 0,   0, 0, 0},
            { 0, 0, 0,   0, 0, 0,   0, 0, 0}
        };
#pragma warning restore S1144 // Unused private types or members should be removed
    }
}
