# Sudoku

## About This Project

This project will implement a Sudoku solver, an application that analyzes a given 
Sudoku puzzle and outputs a report of the steps needed to solve it.


## About Sudoku

Sudoku is a logic-based, combinatorial number-placement puzzle. The objective is to fill 
a 9×9 grid with digits so that each column, each row, and each of the nine 3×3 subgrids that 
compose the grid contain all of the digits from 1 to 9.  The puzzle setter provides a partially 
completed grid, which for a well-posed puzzle has a single solution.  (Wikipedia)


## User Story: Create Sudoku Solver

As a **Sudoku enthusiast** who has a Sudoku puzzle to solve,  
I want a **discover the steps to solve my puzzle**  
So that I can **learn how to solve my puzzle**, and also **learn how to solve other Sudoku puzzles**.


#### Acceptance Criteria

- I can specify the exact puzzle to solve
- A correct, step-by-step solution is generated
- I can follow the solution to manually solve my puzzle
- Each solution step provides enough information to allow validating the logic of the step
- Running the program a second time does not overwrite a previous solution


## About the Solver

The inital Solver will be a console program that:

- Reads the puzzle to be solved from a designated text file
- Displays in the console appropriate summary status about progress to a solution
- Writes the discovered puzzle solution to a designated text file


## Repository Layout

| File / Directory          | Description                                                           |
| :------------------------ | :-------------------------------------------------------------------- |
| Sudoku.sln                | Visual Studio 2017 solution for whole project                         |  
| Sudoku.Library/           | Class library containing gaming engine and solver                     |  
| Sudoku.Library.Tests/     | Unit tests for Sudoku.Library classes                                 |  


## Key Definitions

| Term                | Description                                                                 |
| :------------------ | :-------------------------------------------------------------------------- |
| Grid                | The 9 x 9 array of cells that form the main structure of the puzzle         |  
| Cell                | A single element of the grid that may contain a value or value candidates.  May be identified by a (Row, Column) coordinate |  
| Cell Value          | The single number (1-9) assigned to the cell.  May be empty.                |  
| Cell Candidates     | The possible values of the cell . A subset of the set of numbers 1-9        |
| Row                 | A region of 9 cells that reside horizontally beside each other.  The grid has 9 rows, numbered 1-9, top to bottom. |  
| Column              | A region of 9 cells that reside vertically on top of one another. The grid has 9 columns, numbered 1-9, left to right. |  
| Block               | A 3x3 region of 9 cells at the intersection of Rows/Columns 1-3, 4-6, 7-9.  The grid has 9 blocks, numbered 1-9, in standard reading order. |  
| Region              | Generic term for a row, column, or block.                                   |  
