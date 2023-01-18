using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Library.Game;
using Sudoku.Library.SolverSteps;

namespace Sudoku.Library.Solver
{
    public class NakedSingleStrategy : BaseStrategy, ISolverStrategy
    {
        public class NakedSinglePosition : Position
        {
            public Cell TheCell { get; set; }
        };

        public override Position Find(Grid grid)
        {
            Cell nakedSingle = grid.EmptyCells.FirstOrDefault(x => x.Candidates.Count() == 1);

            if (nakedSingle != null)
            {
                return new NakedSinglePosition()
                {
                    TheCell = nakedSingle
                };
            }

            return null;
        }

        public override SolverStep Apply(Grid grid, Position position)
        {
            if ( !(position is NakedSinglePosition) )
                throw new ArgumentOutOfRangeException(nameof(position), $"must be a {nameof(NakedSinglePosition)}");

            var p = position as NakedSinglePosition;

            NakedSingleStep step = CreateChangedStep(grid, p);

            p.TheCell.Number = step.Candidate;
            p.TheCell.ClearAllCandidates();   // TODO: log these cleared candidates

            Utilities.ClearCandidatesInFilledCellRegions(p.TheCell, step.ClearedCandidates);

            step.GridAfter = CaptureGridState(grid);
            return step;
        }

        private NakedSingleStep CreateChangedStep(Grid grid, NakedSinglePosition position)
        {
            return new NakedSingleStep()
            {
                Location = MakeCellCoordinate(position.TheCell),
                Candidate = position.TheCell.Candidates.First(),
                GridBefore = CaptureGridState(grid)
            };
        }

        protected override SolverStep CreateNotFoundStep(Grid grid)
        {
            return new NakedSingleNotFoundStep()
            {
                GridBefore = CaptureGridState(grid)
            };
        }
    }
}
