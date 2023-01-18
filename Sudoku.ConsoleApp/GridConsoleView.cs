using System.Collections.Generic;
using System.Text;
using Sudoku.Library.Game;

namespace Sudoku.ConsoleApp
{
    public class GridConsoleView
    {
        public IEnumerable<string> Render(Grid grid)
        {
            var lines = new List<string>();

            foreach (GridRow row in grid.Rows)
                PrintGridRow(row, lines);

            lines.Add("===============  |  ===============  |  ===============");

            return lines;
        }

        private static void PrintGridRow(GridRow row, List<string> lines)
        {
            if (row.RowIndex == 0 || row.RowIndex == 3 || row.RowIndex == 6)
                lines.Add("===============  |  ===============  |  ===============");
            else
                lines.Add("                 |                   |                 ");

            var line1 = new StringBuilder();
            var line2 = new StringBuilder();
            var line3 = new StringBuilder();

            foreach (Cell c in row.Cells)
            {
                if (c.Number.HasValue)
                {
                    line1.Append("---");
                    line2.Append($"({c.Number.Value})");
                    line3.Append("---");
                }
                else
                {
                    for (int candidate = 1; candidate <= 3; candidate++)
                    {
                        if (c.IsCandidateSet(candidate))
                            line1.Append(candidate);
                        else
                            line1.Append('-');
                    }

                    for (int candidate = 4; candidate <= 6; candidate++)
                    {
                        if (c.IsCandidateSet(candidate))
                            line2.Append(candidate);
                        else
                            line2.Append('-');
                    }

                    for (int candidate = 7; candidate <= 9; candidate++)
                    {
                        if (c.IsCandidateSet(candidate))
                            line3.Append(candidate);
                        else
                            line3.Append('-');
                    }
                }

                string separator = "   ";
                if (c.Location.ColIndex == 2 || c.Location.ColIndex == 5)
                    separator = "  |  ";

                line1.Append(separator);
                line2.Append(separator);
                line3.Append(separator);
            }

            lines.Add(line1.ToString());
            lines.Add(line2.ToString());
            lines.Add(line3.ToString());
        }
    }
}
