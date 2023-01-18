using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public class XWingStrategy : BaseStrategy, ISolverStrategy
    {
        private class XWingPosition : Position
        {
            public IRegion PrimaryRegion1 { get; set; }
            public IRegion PrimaryRegion2 { get; set; }
            public IRegion IntersectingRegion1 { get; set; }
            public IRegion IntersectingRegion2 { get; set; }
            public int Candidate { get; set; }
        }

        public override Position Find(Grid grid)
        {
            IEnumerable<RegionCandidateDetails> rowDetailsList = Enumerable.Range(1, 9).SelectMany(candidate => grid.Rows.Select(row => new RegionCandidateDetails(row, candidate)));
            IEnumerable<RegionCandidateDetails> colDetailsList = Enumerable.Range(1, 9).SelectMany(candidate => grid.Columns.Select(col => new RegionCandidateDetails(col, candidate)));

            var potentialGroups = rowDetailsList
                                   .Concat(colDetailsList)
                                   .Where(x => x.OccurrenceCount == 2)
                                   .GroupBy(x => new GroupingKey() { Candidate = x.CandidateNumber, CellIndexPattern = x.CellIndexPattern, RegionType = x.Region.TypeLabel })
                                   .Where(x => x.Count() == 2)
                                   .Where(x => GroupIsActionable(x, grid));

            if (potentialGroups.Any())
            {
                var group = potentialGroups.First();
                RegionCandidateDetails details1 = group.ElementAt(0);
                RegionCandidateDetails details2 = group.ElementAt(1);

                int intersectingRegionIndex1 = details1.CellIndexes.ElementAt(0);
                int intersectingRegionIndex2 = details1.CellIndexes.ElementAt(1);

                IRegion intersectingRegion1;
                IRegion intersectingRegion2;

                if (details1.Region is GridRow)
                {
                    intersectingRegion1 = grid.Columns[intersectingRegionIndex1];
                    intersectingRegion2 = grid.Columns[intersectingRegionIndex2];
                }
                else
                {
                    intersectingRegion1 = grid.Rows[intersectingRegionIndex1];
                    intersectingRegion2 = grid.Rows[intersectingRegionIndex2];
                }

                return new XWingPosition
                {
                    PrimaryRegion1 = details1.Region,
                    PrimaryRegion2 = details2.Region,
                    IntersectingRegion1 = intersectingRegion1,
                    IntersectingRegion2 = intersectingRegion2,
                    Candidate = details1.CandidateNumber
                };
            }

            return null;
        }

        public override SolverStep Apply(Grid grid, Position position)
        {
            var p = position as XWingPosition;

            if (p == null)
                throw new ArgumentOutOfRangeException(nameof(position), "position is wrong type");

            XWingStep step = CreateChangedStep(grid, p);

            step.PrimaryRegionNumbers[0] = p.PrimaryRegion1.RegionNumber;
            step.PrimaryRegionNumbers[1] = p.PrimaryRegion2.RegionNumber;

            step.OtherRegionNumbers[0] = p.IntersectingRegion1.RegionNumber;
            step.OtherRegionNumbers[1] = p.IntersectingRegion2.RegionNumber;

            var region1Pairs = p.IntersectingRegion1.EmptyCells.Select(cell => new { Region = p.IntersectingRegion1, Cell = cell });
            var region2Pairs = p.IntersectingRegion2.EmptyCells.Select(cell => new { Region = p.IntersectingRegion2, Cell = cell });

            foreach (var pair in region1Pairs.Concat(region2Pairs))
            {
                if (p.PrimaryRegion1.ContainsCell(pair.Cell) || p.PrimaryRegion2.ContainsCell(pair.Cell))
                    continue;

                if (pair.Cell.ClearCandidate(p.Candidate))
                {
                    step.ClearedCandidates.Add(new ClearedCandidate()
                    {
                        Candidate = p.Candidate,
                        Location = MakeCellCoordinate(pair.Cell),
                        ContextRegionType = MapRegionType(pair.Region),
                        ContextRegionNumber = pair.Region.RegionNumber
                    });
                }
            }

            step.GridAfter = CaptureGridState(grid);

            return step;
        }

        private static bool GroupIsActionable(IGrouping<GroupingKey, RegionCandidateDetails> group, Grid grid)
        {
            RegionCandidateDetails details1 = group.ElementAt(0);
            RegionCandidateDetails details2 = group.ElementAt(1);

            int intersectingRegionIndex1 = details1.CellIndexes.ElementAt(0);
            int intersectingRegionIndex2 = details1.CellIndexes.ElementAt(1);

            IRegion intersectingRegion1;
            IRegion intersectingRegion2;

            if (details1.Region is GridRow)
            {
                intersectingRegion1 = grid.Columns[intersectingRegionIndex1];
                intersectingRegion2 = grid.Columns[intersectingRegionIndex2];
            }
            else
            {
                intersectingRegion1 = grid.Rows[intersectingRegionIndex1];
                intersectingRegion2 = grid.Rows[intersectingRegionIndex2];
            }

            foreach (Cell c in intersectingRegion1.EmptyCells.Concat(intersectingRegion2.EmptyCells))
            {
                if (details1.Region.ContainsCell(c) || details2.Region.ContainsCell(c))
                    continue;

                if (c.IsCandidateSet(details1.CandidateNumber))
                {
                    return true;
                }
            }

            return false;
        }

        private XWingStep CreateChangedStep(Grid grid, XWingPosition position)
        {
            return new XWingStep()
            {
                PrimaryRegionType = MapRegionType(position.PrimaryRegion1),
                Candidate = position.Candidate,
                GridBefore = CaptureGridState(grid)
            };
        }

        protected override SolverStep CreateNotFoundStep(Grid grid)
        {
            return new XWingNotFoundStep()
            {
                GridBefore = CaptureGridState(grid)
            };
        }

        private class GroupingKey
        {
            public int Candidate { get; set; }
            public int CellIndexPattern { get; set; }
            public string RegionType { get; set; }

            public override bool Equals(object obj)
            {
                var key = obj as GroupingKey;

                return key != null &&
                       Candidate == key.Candidate &&
                       CellIndexPattern == key.CellIndexPattern &&
                       RegionType == key.RegionType;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Candidate, CellIndexPattern, RegionType);
            }
        }
    }
}