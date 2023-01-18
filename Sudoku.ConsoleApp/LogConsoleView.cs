using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Library.SolverSteps;

namespace Sudoku.ConsoleApp
{
    public class LogConsoleView
    {
        internal IEnumerable<string> Render(IReadOnlyList<SolverStep> log)
        {
            var messages = new List<string>();

            foreach (var step in log)
            {
                if (step is AssumeCandidatesStep)
                {
                    RenderAssumeCandidatesStep(step as AssumeCandidatesStep, messages);
                }
                else if (step is AssumeCandidatesNotFoundStep)
                {
                    RenderAssumeCandidatesNotFoundStep(step as AssumeCandidatesNotFoundStep, messages);
                }
                else if (step is RemoveInvalidCandidatesStep)
                {
                    RenderRemoveInvalidCandidatesStep(step as RemoveInvalidCandidatesStep, messages);
                }
                else if (step is RemoveInvalidCandidatesNotFoundStep)
                {
                    RenderRemoveInvalidCandidatesNotFoundStep(step as RemoveInvalidCandidatesNotFoundStep, messages);
                }
                else if (step is NakedSingleStep)
                {
                    RenderNakedSingleStep(step as NakedSingleStep, messages);
                }
                else if (step is NakedSingleNotFoundStep)
                {
                    RenderNakedSingleNotFoundStep(step as NakedSingleNotFoundStep, messages);
                }
                else if (step is NakedDoubleStep)
                {
                    RenderNakedDoubleStep(step as NakedDoubleStep, messages);
                }
                else if (step is NakedDoubleNotFoundStep)
                {
                    RenderNakedDoubleNotFoundStep(step as NakedDoubleNotFoundStep, messages);
                }
                else if (step is HiddenSingleStep)
                {
                    RenderHiddenSingleStep(step as HiddenSingleStep, messages);
                }
                else if (step is HiddenSingleNotFoundStep)
                {
                    RenderHiddenSingleNotFoundStep(step as HiddenSingleNotFoundStep, messages);
                }
                else if (step is HiddenDoubleStep)
                {
                    RenderHiddenDoubleStep(step as HiddenDoubleStep, messages);
                }
                else if (step is HiddenDoubleNotFoundStep)
                {
                    RenderHiddenDoubleNotFoundStep(step as HiddenDoubleNotFoundStep, messages);
                }
                else if (step is PointingIntersectionStep)
                {
                    RenderPointingIntersectionStep(step as PointingIntersectionStep, messages);
                }
                else if (step is PointingIntersectionNotFoundStep)
                {
                    RenderPointingIntersectionNotFoundStep(step as PointingIntersectionNotFoundStep, messages);
                }
                else if (step is ClaimingIntersectionStep)
                {
                    RenderClaimingIntersectionStep(step as ClaimingIntersectionStep, messages);
                }
                else if (step is ClaimingIntersectionNotFoundStep)
                {
                    RenderClaimingIntersectionNotFoundStep(step as ClaimingIntersectionNotFoundStep, messages);
                }
                else if (step is NakedTripleStep)
                {
                    RenderNakedTripleStep(step as NakedTripleStep, messages);
                }
                else if (step is NakedTripleNotFoundStep)
                {
                    RenderNakedTripleNotFoundStep(step as NakedTripleNotFoundStep, messages);
                }
                else if (step is XWingStep)
                {
                    RenderXWingStep(step as XWingStep, messages);
                }
                else if (step is XWingNotFoundStep)
                {
                    RenderXWingNotFoundStep(step as XWingNotFoundStep, messages);
                }
                else
                    throw new NotImplementedException($"Log message type ({step.GetType().Name} : SolverStep) not implemented");

                if (step is AssumeCandidatesStep || step is ClearingSolverStep)
                {
                    messages.Add("");
                    RenderBeforeAfterGrid(step, messages, true);
                }

                messages.Add("");
            }

            return messages;
        }

        private void RenderAssumeCandidatesStep(AssumeCandidatesStep step, List<string> messages)
        {
            messages.Add("Assume all numbers 1-9 are candidates for empty cells");

            var groups = step.Locations.GroupBy(x => x.RowNumber);

            StringBuilder sb = new StringBuilder();
            foreach (var group in groups.OrderBy(x => x.Key))
            {
                sb.Clear();

                sb.Append($"  Setting 1-9 in Row {group.Key}:");

                foreach (var location in group.OrderBy(x => x.ColNumber))
                    sb.Append($" {Cell2Str(location)}");

                messages.Add(sb.ToString());
            }
        }

        private void RenderAssumeCandidatesNotFoundStep(AssumeCandidatesNotFoundStep step, List<string> messages)
        {
            messages.Add("Couldn't find any empty cells to assume candidates in.");
        }

        private void RenderRemoveInvalidCandidatesStep(RemoveInvalidCandidatesStep step, List<string> messages)
        {
            messages.Add($"Eliminate candidates given pre-set value {step.Number} in Cell {Cell2Str(step.Location)}");

            var groups = step.ClearedCandidates
                    .GroupBy(x => new { RegionType = x.ContextRegionType, RegionNumber = x.ContextRegionNumber });

            StringBuilder sb = new StringBuilder();
            foreach (var group in groups.OrderBy(x => x.Key.RegionType).ThenBy(x => x.Key.RegionNumber))
            {
                sb.Clear();

                sb.Append($"  Clearing {step.Number} from {group.Key.RegionType} {group.Key.RegionNumber}:");

                foreach (var cell in group.OrderBy(x => x.Location.RowNumber).ThenBy(x => x.Location.ColNumber))
                    sb.Append($" {Cell2Str(cell.Location)}");

                messages.Add(sb.ToString());
            }
        }

        private void RenderRemoveInvalidCandidatesNotFoundStep(RemoveInvalidCandidatesNotFoundStep step, List<string> messages)
        {
            messages.Add("Couldn't find any filled cells help eliminate candidates."); 
        }

        private void RenderNakedSingleStep(NakedSingleStep step, List<string> messages)
        {
            messages.Add($"Found Naked Single in Cell {Cell2Str(step.Location)}, candidate {step.Candidate}");
            messages.Add($"  Setting Cell {Cell2Str(step.Location)} value to {step.Candidate}");

            var groups = step.ClearedCandidates
                    .GroupBy(x => new { RegionType = x.ContextRegionType, RegionNumber = x.ContextRegionNumber });

            StringBuilder sb = new StringBuilder();
            foreach (var group in groups.OrderBy(x => x.Key.RegionType))
            {
                sb.Clear();

                sb.Append($"  Clearing {step.Candidate} from {group.Key.RegionType} {group.Key.RegionNumber}:");

                foreach (var cell in group.OrderBy(x => x.Location.RowNumber).ThenBy(x => x.Location.ColNumber))
                    sb.Append($" {Cell2Str(cell.Location)}");

                messages.Add(sb.ToString());
            }
        }

        private void RenderNakedSingleNotFoundStep(NakedSingleNotFoundStep step, List<string> messages)
        {
            messages.Add("Couldn't find any Naked Singles");
        }

        private void RenderNakedDoubleStep(NakedDoubleStep step, List<string> messages)
        {
            messages.Add($"Found Naked Double in {step.PrimaryRegionType} {step.PrimaryRegionNumber}, Cells {Cell2Str(step.Locations[0])} {Cell2Str(step.Locations[1])}, candidates {string.Join(" and ", step.Candidates)}");

            var groups = step.ClearedCandidates
                    .GroupBy(x => x.Candidate);

            StringBuilder sb = new StringBuilder();
            foreach (var group in groups.OrderBy(x => x.Key))
            {
                sb.Clear();

                sb.Append($"  Clearing {group.Key} from {step.PrimaryRegionType} {step.PrimaryRegionNumber}:");

                foreach (var cell in group.OrderBy(x => x.Location.RowNumber).ThenBy(x => x.Location.ColNumber))
                    sb.Append($" {Cell2Str(cell.Location)}");

                messages.Add(sb.ToString());
            }
        }

        private void RenderNakedDoubleNotFoundStep(NakedDoubleNotFoundStep step, List<string> messages)
        {
            messages.Add("Couldn't find any Naked Doubles");
        }

        private void RenderHiddenSingleStep(HiddenSingleStep step, List<string> messages)
        {
            messages.Add($"Found Hidden Single in {step.PrimaryRegionType} {step.PrimaryRegionNumber}, Cell {Cell2Str(step.Location)}, candidate {step.Candidate}");
            messages.Add($"  Setting Cell {Cell2Str(step.Location)} value to {step.Candidate}");

            var groups = step.ClearedCandidates
                    .GroupBy(x => new { RegionType = x.ContextRegionType, RegionNumber = x.ContextRegionNumber });

            StringBuilder sb = new StringBuilder();
            foreach (var group in groups.OrderBy(x => x.Key.RegionType))
            {
                sb.Clear();

                sb.Append($"  Clearing {step.Candidate} from {group.Key.RegionType} {group.Key.RegionNumber}:");

                foreach (var cell in group.OrderBy(x => x.Location.RowNumber).ThenBy(x => x.Location.ColNumber))
                    sb.Append($" {Cell2Str(cell.Location)}");

                messages.Add(sb.ToString());
            }
        }

        private void RenderHiddenSingleNotFoundStep(HiddenSingleNotFoundStep step, List<string> messages)
        {
            messages.Add("Couldn't find any Hidden Singles");
        }

        private void RenderHiddenDoubleStep(HiddenDoubleStep step, List<string> messages)
        {
            messages.Add($"Found Hidden Double in {step.PrimaryRegionType} {step.PrimaryRegionNumber}, Cells {Cell2Str(step.Locations[0])} {Cell2Str(step.Locations[1])}, candidates {string.Join(" and ", step.Candidates)}");

            var groups = step.ClearedCandidates
                    .GroupBy(x => x.Candidate);

            StringBuilder sb = new StringBuilder();
            foreach (var group in groups.OrderBy(x => x.Key))
            {
                sb.Clear();

                sb.Append($"  Clearing {group.Key} from {step.PrimaryRegionType} {step.PrimaryRegionNumber}:");

                foreach (var cell in group.OrderBy(x => x.Location.RowNumber).ThenBy(x => x.Location.ColNumber))
                    sb.Append($" {Cell2Str(cell.Location)}");

                messages.Add(sb.ToString());
            }
        }

        private void RenderHiddenDoubleNotFoundStep(HiddenDoubleNotFoundStep step, List<string> messages)
        {
            messages.Add("Couldn't find any Hidden Doubles");
        }

        private void RenderPointingIntersectionStep(PointingIntersectionStep step, List<string> messages)
        {
            messages.Add($"Found Pointing Intersection between {step.PrimaryRegionType} {step.PrimaryRegionBlockNumber} and {step.OtherRegionType} {step.OtherRegionNumber}, candidate {step.Candidate}");

            var groups = step.ClearedCandidates
                    .GroupBy(x => x.Candidate);

            StringBuilder sb = new StringBuilder();
            foreach (var group in groups.OrderBy(x => x.Key))
            {
                sb.Clear();

                sb.Append($"  Clearing {group.Key} from {step.OtherRegionType} {step.OtherRegionNumber}:");

                foreach (var cell in group.OrderBy(x => x.Location.RowNumber).ThenBy(x => x.Location.ColNumber))
                    sb.Append($" {Cell2Str(cell.Location)}");

                messages.Add(sb.ToString());
            }
        }

        private void RenderPointingIntersectionNotFoundStep(PointingIntersectionNotFoundStep step, List<string> messages)
        {
            messages.Add("Couldn't find any Pointing Intersections");
        }

        private void RenderClaimingIntersectionStep(ClaimingIntersectionStep step, List<string> messages)
        {
            messages.Add($"Found Claiming Intersection between {step.PrimaryRegionType} {step.PrimaryRegionNumber} and {step.OtherRegionType} {step.OtherRegionBlockNumber}, candidate {step.Candidate}");

            var groups = step.ClearedCandidates
                    .GroupBy(x => x.Candidate);

            StringBuilder sb = new StringBuilder();
            foreach (var group in groups.OrderBy(x => x.Key))
            {
                sb.Clear();

                sb.Append($"  Clearing {group.Key} from {step.OtherRegionType} {step.OtherRegionBlockNumber}:");

                foreach (var cell in group.OrderBy(x => x.Location.RowNumber).ThenBy(x => x.Location.ColNumber))
                    sb.Append($" {Cell2Str(cell.Location)}");

                messages.Add(sb.ToString());
            }
        }

        private void RenderClaimingIntersectionNotFoundStep(ClaimingIntersectionNotFoundStep step, List<string> messages)
        {
            messages.Add("Couldn't find any Claiming Intersections");
        }

        private void RenderNakedTripleStep(NakedTripleStep step, List<string> messages)
        {
            messages.Add($"Found Naked Triple in {step.PrimaryRegionType} {step.PrimaryRegionNumber}, Cells {Cell2Str(step.Locations[0])} {Cell2Str(step.Locations[1])} {Cell2Str(step.Locations[2])}, candidates {string.Join(", ", step.Candidates)}");

            var groups = step.ClearedCandidates
                    .GroupBy(x => x.Candidate);

            StringBuilder sb = new StringBuilder();
            foreach (var group in groups.OrderBy(x => x.Key))
            {
                sb.Clear();

                sb.Append($"  Clearing {group.Key} from {step.PrimaryRegionType} {step.PrimaryRegionNumber}:");

                foreach (var cell in group.OrderBy(x => x.Location.RowNumber).ThenBy(x => x.Location.ColNumber))
                    sb.Append($" {Cell2Str(cell.Location)}");

                messages.Add(sb.ToString());
            }
        }

        private void RenderNakedTripleNotFoundStep(NakedTripleNotFoundStep step, List<string> messages)
        {
            messages.Add("Couldn't find any Naked Triples");
        }

        private void RenderXWingStep(XWingStep step, List<string> messages)
        {
            messages.Add($"Found X-Wing between {step.PrimaryRegionType}s {step.PrimaryRegionNumbers[0]} and {step.PrimaryRegionNumbers[1]} against {step.OtherRegionType}s {step.OtherRegionNumbers[0]} and {step.OtherRegionNumbers[1]}, candidate {step.Candidate}");

            var groups = step.ClearedCandidates
                    .GroupBy(x => new { RegionType = x.ContextRegionType, RegionNumber = x.ContextRegionNumber });

            StringBuilder sb = new StringBuilder();
            foreach (var group in groups.OrderBy(x => x.Key.RegionNumber))
            {
                sb.Clear();

                sb.Append($"  Clearing {step.Candidate} from {group.Key.RegionType} {group.Key.RegionNumber}:");

                foreach (var cell in group.OrderBy(x => x.Location.RowNumber).ThenBy(x => x.Location.ColNumber))
                    sb.Append($" {Cell2Str(cell.Location)}");

                messages.Add(sb.ToString());
            }
        }

        private void RenderXWingNotFoundStep(XWingNotFoundStep step, List<string> messages)
        {
            messages.Add("Couldn't find any X-Wings");
        }

        private string Cell2Str(CellCoordinate coord)
        {
            return $"(R{coord.RowNumber},C{coord.ColNumber})";
        }

        private void RenderBeforeAfterGrid(SolverStep step, List<string> messages, bool renderAfter)
        {
            for (int rowIndex=0; rowIndex<=step.GridBefore.GetUpperBound(0); rowIndex++)
            {
                if (rowIndex == 0 || rowIndex == 3 || rowIndex == 6)
                    messages.Add($"{SolidBorder}{HorizontalSeparator}{(renderAfter ? SolidBorder : string.Empty)}");
                else
                    messages.Add($"{WhiteBorder}{HorizontalSeparator}{(renderAfter ? WhiteBorder : string.Empty)}");

                var line1 = new StringBuilder();
                var line2 = new StringBuilder();
                var line3 = new StringBuilder();

                for (int colIndex = 0; colIndex <= step.GridBefore.GetUpperBound(1); colIndex++)
                {
                    CellState cell = step.GridBefore[rowIndex, colIndex];
                    RenderRow(line1, line2, line3, colIndex, cell);
                }

                if (renderAfter && step is ChangingSolverStep)
                {
                    line1.Append(HorizontalSeparator);
                    line2.Append(HorizontalSeparator);
                    line3.Append(HorizontalSeparator);

                    var changingStep = step as ChangingSolverStep;

                    for (int colIndex = 0; colIndex <= step.GridBefore.GetUpperBound(1); colIndex++)
                    {
                        CellState cell = changingStep.GridAfter[rowIndex, colIndex];
                        RenderRow(line1, line2, line3, colIndex, cell);
                    }
                }

                messages.Add(line1.ToString());
                messages.Add(line2.ToString());
                messages.Add(line3.ToString());
            }

            messages.Add($"{SolidBorder}{HorizontalSeparator}{(renderAfter ? SolidBorder : string.Empty)}");
        }

        private static void RenderRow(StringBuilder line1, StringBuilder line2, StringBuilder line3, int colIndex, CellState cell)
        {
            if (cell.Number.HasValue)
            {
                line1.Append("---");
                line2.Append($"({cell.Number.Value})");
                line3.Append("---");
            }
            else
            {
                for (int candidate = 1; candidate <= 3; candidate++)
                {
                    if (cell.Candidates.Contains(candidate))
                        line1.Append(candidate);
                    else
                        line1.Append('-');
                }

                for (int candidate = 4; candidate <= 6; candidate++)
                {
                    if (cell.Candidates.Contains(candidate))
                        line2.Append(candidate);
                    else
                        line2.Append('-');
                }

                for (int candidate = 7; candidate <= 9; candidate++)
                {
                    if (cell.Candidates.Contains(candidate))
                        line3.Append(candidate);
                    else
                        line3.Append('-');
                }
            }

            if (colIndex < 8)
            {
                string separator = "   ";
                if (colIndex == 2 || colIndex == 5)
                    separator = "  |  ";

                line1.Append(separator);
                line2.Append(separator);
                line3.Append(separator);
            }
        }

        private const string SolidBorder = "===============  |  ===============  |  ===============";
        private const string WhiteBorder = "                 |                   |                 ";
        private const string HorizontalSeparator = "        ";
    }
}