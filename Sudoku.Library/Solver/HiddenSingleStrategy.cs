using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public class HiddenSingleStrategy : BaseStrategy, ISolverStrategy
    {
        private class HiddenSinglePosition : Position
        {
            public IRegion Region;
            public int Candidate;
            public Cell TheCell;
        };

        public override Position Find(Grid grid)
        {
            // compute regionCandidateDetails for each combination of region/candidate number
            IEnumerable<RegionCandidateDetails> regionCandidateDetails = 
                grid.Regions.SelectMany(r => Enumerable.Range(1, 9).Select(c => new RegionCandidateDetails(r, c)));

            RegionCandidateDetails hiddenSingleDetails = regionCandidateDetails.FirstOrDefault(
                x => x.OccurrenceCount == 1 && x.Region.Cells[x.CellIndexes.First()].Candidates.Count()>1);

            if (hiddenSingleDetails != null)
            {
                return new HiddenSinglePosition()
                {
                    Candidate = hiddenSingleDetails.CandidateNumber,
                    TheCell = hiddenSingleDetails.Region.Cells[hiddenSingleDetails.CellIndexes.First()],
                    Region = hiddenSingleDetails.Region
                };
            }

            return null;
        }

        public override SolverStep Apply(Grid grid, Position position)
        {
            var p = position as HiddenSinglePosition;

            if (p == null)
                throw new ArgumentOutOfRangeException(nameof(position), "Wrong position type");

            HiddenSingleStep step = CreateChangedStep(grid, p);

            p.TheCell.Number = p.Candidate;
            p.TheCell.ClearAllCandidates();  // TODO: log these cleared candidates

            Utilities.ClearCandidatesInFilledCellRegions(p.TheCell, step.ClearedCandidates);

            step.GridAfter = CaptureGridState(grid);
            return step;
        }

        private HiddenSingleStep CreateChangedStep(Grid grid, HiddenSinglePosition position)
        {
            return new HiddenSingleStep()
            {
                PrimaryRegionType = MapRegionType(position.Region),
                PrimaryRegionNumber = position.Region.RegionNumber,
                Location = MakeCellCoordinate(position.TheCell),
                Candidate = position.Candidate,
                GridBefore = CaptureGridState(grid)
            };
        }

        protected override SolverStep CreateNotFoundStep(Grid grid)
        {
            return new HiddenSingleNotFoundStep()
            {
                GridBefore = CaptureGridState(grid)
            };
        }
    }
}
