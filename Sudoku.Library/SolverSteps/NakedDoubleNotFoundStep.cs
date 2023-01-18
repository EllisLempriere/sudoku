namespace Sudoku.Library.SolverSteps
{
    /// <summary>
    /// Represents a solution search of this type that yielded no results
    /// </summary>
    public class NakedDoubleNotFoundStep : SolverStep
    {
        public override bool Equals(object obj)
        {
            var step = obj as NakedDoubleNotFoundStep;

            return step != null && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
