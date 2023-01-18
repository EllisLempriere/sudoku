
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

using System.Runtime.CompilerServices;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style",
    "IDE0028:Simplify collection initialization",
    Justification = "Generally yes, but in this case no, for consistency/readability in method",
    Scope = "member",
    Target = "~M:Sudoku.Library.Solver.GridSolver.SetInitialCandidates(Sudoku.Library.Game.Grid)~System.Collections.Generic.IEnumerable{System.String}")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style", 
    "IDE0042:Deconstruct variable declaration", 
    Justification = "Team style says this is discretionary.  Prefer not in this case, for readability.", 
    Scope = "member", 
    Target = "~M:Sudoku.Library.Puzzles.GridBuilder.Build~Sudoku.Library.Game.Grid")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style", 
    "IDE0039:Use local function", 
    Justification = "Rule contravenes team style", 
    Scope = "module")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Major Code Smell", 
    "S4144:Methods should not have identical implementations", 
    Justification = "Keep separate for naming/documentation value>", 
    Scope = "member", 
    Target = "~M:Sudoku.Library.Solver.RegionCandidateDetails.RowColumnCandidateIsLocatedInOneBlock~System.Boolean")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Major Code Smell", 
    "S4144:Methods should not have identical implementations",
    Justification = "Keep separate for naming/documentation value>",
    Scope = "member", 
    Target = "~M:Sudoku.Library.Solver.RegionCandidateDetails.GetRowColumnCandidateBlockOffset~System.Int32")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Blocker Code Smell", 
    "S2368:Public methods should not have multidimensional array parameters",
    Justification = "Rule contravenes team style",
    Scope = "module")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style", 
    "IDE0019:Use pattern matching", 
    Justification = "Rule contravenes team style",
    Scope = "module")]

[assembly: InternalsVisibleTo("Sudoku.Library.Tests")]

