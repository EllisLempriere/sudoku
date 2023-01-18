# TO DO list

## Quick fixes  
- Review LogConsoleView for opportunities to extract common code
- Grid,Block,Row,Column EmptyCells/FilledCells reimplement in linq to make more brief

## Medium sized effort
- Consider renaming SolverStep classes to SolverEvent  
  -   LogConsoleView  
  -   GridSolver

- log clearing of candidates in 
  - HiddenSingleStrategy
  - NakedSingleStrategy
  
- CellTests: add tests to cover Cell class, e.g  
  - public void Candidates_NewCell_IsEmpty()
  - public void SetCandidates_AllCandidates_CandidatesIsFullSet()

- Sample out Grid and Log Console View Tests
- Make solver depend on IGrid instead of Grid

## Large changes
- Solver rule tests

- Clarify/homogenize rule implementation

- Separate searching for rule conditions from acting on the rule
  - rule class has enumerator that returns sequence of locations rule could be tested at
  - rule class has IsMatch?() method which takes location and returns bool (match or not)
  - rule class has Apply() method which takes location and modifies grid

