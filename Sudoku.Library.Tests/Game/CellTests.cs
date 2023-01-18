using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Sudoku.Library.Game;

namespace Sudoku.Library.Tests.Game
{
    [TestFixture]
    public class CellTests
    {
        private Cell _basicCell;

        [SetUp]
        public void TestCaseSetup()
        {
            const int arbitraryRowIndex = 6;
            const int arbitraryColIndex = 3;

            _basicCell = new Cell(arbitraryRowIndex, arbitraryColIndex);

            _basicCell.Row = new GridRow();
            _basicCell.Column = new GridColumn();
            _basicCell.Block = new GridBlock();
        }

        // Properties Tests
        // Property: Constructor
        [TestCase(0, 0)]
        [TestCase(8, 8)]
        [TestCase(0, 8)]
        [TestCase(8, 0)]
        [TestCase(3, 5)]
        public void Constructor_ValidCoordinates_ReturnsInstance(int validRowIndex, int validColIndex)
        {
            // arrange

            // act
            Cell cell = new Cell(validRowIndex, validColIndex);

            // assert
            Assert.IsNotNull(cell);
        }

        [TestCase(-1, 0)]
        [TestCase(9, 0)]
        [TestCase(0, -1)]
        [TestCase(0, 9)]
        public void Constructor_InvalidCoordinates_ThrowsException(int rowIndex, int colIndex)
        {
            // arrange

            // act
            TestDelegate act = () => new Cell(rowIndex, colIndex);

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        
        // Property: Location
        [TestCase(2, 3)]
        [TestCase(0, 3)]
        [TestCase(2, 8)]
        public void Location_Get_ReturnsCorrectValues(int rowIndex, int colIndex)
        {
            // arrange
            Cell c = new Cell(rowIndex, colIndex);

            // act
            var location = c.Location;

            // assert
            Assert.AreEqual(rowIndex, location.RowIndex);
            Assert.AreEqual(colIndex, location.ColIndex);
        }


        // Property: Number
        [Test]
        public void Number_SetToNull_ReturnsNull()
        {
            // arrange

            // act
            _basicCell.Number = null;

            // assert
            Assert.IsNull(_basicCell.Number);
        }

        [Test]
        public void Number_SetPopulatedCellToNull_ReturnsNull()
        {
            // arrange
            int? arbitraryValue = 5;
            _basicCell.Number = arbitraryValue;
            Assume.That(_basicCell.Number != null);

            // act
            _basicCell.Number = null;

            // assert
            Assert.IsNull(_basicCell.Number);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void Number_SetValidValue_ReturnsSameValue(int? validValue)
        {
            // arrange

            // act
            _basicCell.Number = validValue;

            // assert
            Assert.AreEqual(validValue, _basicCell.Number);
        }

        [TestCase(0)]
        [TestCase(10)]
        public void Number_SetInvalidValue_ThrowsException(int? invalidValue)
        {
            // arrange

            // act
            TestDelegate act = () => _basicCell.Number = invalidValue;

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }


        // Property: Candidates
        [Test]
        public void Candidates_EmptyCell_CandidatesIsEmpty()
        {
            // arrange

            // act
            
            // assert
            Assert.IsEmpty(_basicCell.Candidates);
        }

        [Test]
        public void Candidates_SingleCandidate_ReturnsCorrectValue()
        {
            // arrange
            int arbitraryCandidate = 7;
            _basicCell.SetCandidate(arbitraryCandidate);

            // act
            bool candidateSet = _basicCell.Candidates.Contains(arbitraryCandidate);

            // assert
            Assert.IsTrue(candidateSet);
        }

        [TestCase(new[] { 1, 4, 6, 9 }, new[] { 1, 4, 6, 9 })]
        [TestCase(new[] { 9, 6, 4, 1 }, new[] { 1, 4, 6, 9 })]
        [TestCase(new[] { 4, 9, 1, 6 }, new[] { 1, 4, 6, 9 })]
        public void Candidates_MultipleCandidatesInDifferentOrders_ReturnsCorrectValues(
            IEnumerable<int> candidatesToSet, IEnumerable<int> expectedValues)
        {
            // arrange
            _basicCell.SetCandidates(candidatesToSet);

            // act

            // assert
            CollectionAssert.AreEquivalent(expectedValues, _basicCell.Candidates);
        }


        // Property: Regions
        [Test]
        public void Regions_SetRow_ContainsRow()
        {
            // arrange
            GridRow stub = new GridRow();
            Assume.That(_basicCell.Regions.Contains(stub), Is.False);
            Assume.That(_basicCell.Row != stub);

            // act
            _basicCell.Row = stub;

            // assert
            Assert.IsTrue(_basicCell.Regions.Contains(stub));
            Assert.AreEqual(stub, _basicCell.Row);
        }

        [Test]
        public void Regions_SetColumn_ContainsColumn()
        {
            // arrange
            GridColumn stub = new GridColumn();
            Assume.That(_basicCell.Regions.Contains(stub), Is.False);
            Assume.That(_basicCell.Column != stub);

            // act
            _basicCell.Column = stub;

            // assert
            Assert.IsTrue(_basicCell.Regions.Contains(stub));
            Assert.AreEqual(stub, _basicCell.Column);
        }

        [Test]
        public void Regions_SetBlock_ContainsBlock()
        {
            // arrange
            GridBlock stub = new GridBlock();
            Assume.That(_basicCell.Regions.Contains(stub), Is.False);
            Assume.That(_basicCell.Block != stub);

            // act
            _basicCell.Block = stub;

            // assert
            Assert.IsTrue(_basicCell.Regions.Contains(stub));
            Assert.AreEqual(stub, _basicCell.Block);
        }

        [Test]
        public void Regions_SetRowColumnBlock_ContainsRowColumnBlock()
        {
            // arrange
            GridRow stubRow = new GridRow();
            GridColumn stubColumn = new GridColumn();
            GridBlock stubBlock = new GridBlock();

            Assume.That(_basicCell.Regions.Contains(stubRow), Is.False);
            Assume.That(_basicCell.Regions.Contains(stubColumn), Is.False);
            Assume.That(_basicCell.Regions.Contains(stubBlock), Is.False);

            Assume.That(_basicCell.Row != stubRow);
            Assume.That(_basicCell.Column != stubColumn);
            Assume.That(_basicCell.Block != stubBlock);


            // act
            _basicCell.Row = stubRow;
            _basicCell.Column = stubColumn;
            _basicCell.Block = stubBlock;

            // assert
            Assert.IsTrue(_basicCell.Regions.Contains(stubRow));
            Assert.IsTrue(_basicCell.Regions.Contains(stubColumn));
            Assert.IsTrue(_basicCell.Regions.Contains(stubBlock));

            Assert.AreEqual(stubRow, _basicCell.Row);
            Assert.AreEqual(stubColumn, _basicCell.Column);
            Assert.AreEqual(stubBlock, _basicCell.Block);
        }

        [TestCase(false, false, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        [TestCase(false, false, true)]
        public void Regions_IncompleteInitialization_ReadThrowsException(bool isRowSet, bool isColumnSet, bool isBlockSet)
        {
            // arrange
            _basicCell.Row = isRowSet ? new GridRow() : null;
            _basicCell.Column = isColumnSet ? new GridColumn() : null;
            _basicCell.Block = isBlockSet ? new GridBlock() : null;

            // act
            TestDelegate act = () => _basicCell.Regions.Count();

            // assert
            Assert.Throws<InvalidOperationException>(act);
        }



        // Method Tests
        // Method: SetCandidate
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void SetCandidate_ValidValue_IsCandidateSetReturnsTrue(int validCandidate)
        {
            // arrange
            Assume.That(_basicCell.Candidates.Contains(validCandidate), Is.False);

            // act
            bool cellChanged = _basicCell.SetCandidate(validCandidate);

            // assert
            Assert.IsTrue(cellChanged);
            Assert.IsTrue(_basicCell.IsCandidateSet(validCandidate));
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(8)]
        public void SetCandidate_SetSameValueTwice_ValueSavedOnce(int validCandidate)
        {
            // arrange
            Assume.That(_basicCell.Candidates.Contains(validCandidate), Is.False);
            Assume.That(_basicCell.Candidates.Count() == 0);

            // act
            bool cellChanged1 = _basicCell.SetCandidate(validCandidate);
            bool cellChanged2 = _basicCell.SetCandidate(validCandidate);

            // assert
            Assert.IsTrue(cellChanged1);
            Assert.IsFalse(cellChanged2);
            Assert.IsTrue(_basicCell.IsCandidateSet(validCandidate));
            Assert.AreEqual(1, _basicCell.Candidates.Count());
        }

        [TestCase(0)]
        [TestCase(10)]
        public void SetCandidate_InvalidValue_ThrowsException(int invalidCandidateValue)
        {
            // arrange

            // act
            TestDelegate act = () => _basicCell.SetCandidate(invalidCandidateValue);

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }


        // Method: ClearCandidate
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void ClearCandidate_ValidSetValue_CellChangedAndValueCleared(int validCandidateValue)
        {
            // arrange
            _basicCell.SetCandidate(validCandidateValue);
            Assume.That(_basicCell.IsCandidateSet(validCandidateValue));

            // act
            bool cellChanged = _basicCell.ClearCandidate(validCandidateValue);

            // assert
            Assert.IsTrue(cellChanged);
            Assert.IsFalse(_basicCell.IsCandidateSet(validCandidateValue));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(7)]
        public void ClearCandidate_ValidUnsetValue_CellUnchangedAndValueClear(int validCandidateValue)
        {
            // arrange
            Assume.That(!_basicCell.IsCandidateSet(validCandidateValue));

            // act
            bool cellChanged = _basicCell.ClearCandidate(validCandidateValue);

            // assert
            Assert.IsFalse(cellChanged);
            Assert.IsFalse(_basicCell.IsCandidateSet(validCandidateValue));
        }

        [TestCase(0)]
        [TestCase(10)]
        public void ClearCandidate_InvalidValue_ThrowsException(int invalidCandidateValue)
        {
            // arrange

            // act
            TestDelegate act = () => _basicCell.ClearCandidate(invalidCandidateValue);

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }


        // Method: SetCandidates
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, "No duplicates, ascending order")]
        [TestCase(new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 }, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, "No duplicates, descending order")]
        [TestCase(new[] { 1, 5, 5, 7, 9, 9 }, new[] { 1, 5, 7, 9 }, "Duplicates, ascending order")]
        [TestCase(new[] { 5, 9, 1, 9, 5, 7 }, new[] { 1, 5, 7, 9 }, "Duplicates, mixed order")]
        [TestCase(new[] { 4, 4, 4 }, new[] { 4 }, "All duplicates")]
        public void SetCandidates_ValidValues_CellChangedAndCandidatesSet(
            IEnumerable<int> validCandidates, IEnumerable<int> expectedCandidates, string testCaseDescription)
        {
            // arrange
            Assume.That(_basicCell.Candidates.Count() == 0);

            // act
            bool cellChanged = _basicCell.SetCandidates(validCandidates);

            // assert
            Assert.IsTrue(cellChanged, $"Unexpected value of {nameof(cellChanged)} for test case '{testCaseDescription}'");
            CollectionAssert.AreEquivalent(expectedCandidates, _basicCell.Candidates, $"Unexpected final candidates for test case '{testCaseDescription}'");
        }

        [TestCase(new[] { 2 }, new[] { 1 }, new[] { 1, 2 }, "No overlap")]
        [TestCase(new[] { 3, 5 }, new[] { 1, 3, 4 }, new[] { 1, 3, 4, 5 }, "Some overlap")]
        [TestCase(new[] { 2, 4, 6, 8 }, new[] { 4, 8 }, new[] { 2, 4, 6, 8 }, "Full overlap")]
        public void SetCandidates_AddToPreexistingCandidates_NewCandidatesAdded(
            IEnumerable<int> presetCandidates, IEnumerable<int> candidatesToSet, IEnumerable<int> expectedCandidates, string testCaseDescription)
        {
            // arrange
            _basicCell.SetCandidates(presetCandidates);
            Assume.That(_basicCell.Candidates.Count() == presetCandidates.Count());
            bool expectedCellChanged = !Enumerable.SequenceEqual(presetCandidates, expectedCandidates);

            // act
            bool cellChanged = _basicCell.SetCandidates(candidatesToSet);

            // assert
            Assert.AreEqual(expectedCellChanged, cellChanged, $"Unexpected value of {nameof(cellChanged)} for test case '{testCaseDescription}'");
            CollectionAssert.AreEquivalent(expectedCandidates, _basicCell.Candidates, $"Unexpected final candidates for test case '{testCaseDescription}'");
        }

        [Test]
        public void SetCandidates_SetSameValuesTwice_ValueSavedOnce()
        {
            // arrange
            IEnumerable<int> arbitraryValidCandidates = new[]
            {
                2, 4, 6, 8
            };

            // act
            bool cellChanged1 = _basicCell.SetCandidates(arbitraryValidCandidates);
            bool cellChanged2 = _basicCell.SetCandidates(arbitraryValidCandidates);

            // assert
            Assert.IsTrue(cellChanged1);
            Assert.IsFalse(cellChanged2);
            CollectionAssert.AreEquivalent(arbitraryValidCandidates, _basicCell.Candidates);
        }

        [TestCase(new[] { 0, 1 })]
        [TestCase(new[] { 1, 0 })]
        [TestCase(new[] { 9, 10 })]
        [TestCase(new[] { 10, 9 })]
        public void SetCandidates_ValidAndInvalidCandidates_ThowsException(IEnumerable<int> validAndInvalidCandidates)
        {
            // arrange

            // act
            TestDelegate act = () => _basicCell.SetCandidates(validAndInvalidCandidates);

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }


        // Method: ClearCandidates
        [TestCase(new[] { 2, 4, 6 }, new[] { 2, 4 }, new[] { 6 }, "Clear values that are preset, leave one")]
        [TestCase(new[] { 2, 4, 6 }, new[] { 1, 4, 9 }, new int[] { 2, 6 }, "Clear one set and two unset values, leave two values")]
        [TestCase(new[] { 2, 4, 6 }, new[] { 2, 4, 6, 7 }, new int[] { }, "Clear all preset values, plus unset value")]
        [TestCase(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 2, 4, 6 }, "Clear no preset values")]
        [TestCase(new[] { 2, 4, 6 }, new[] { 7, 8, 9 }, new[] { 2, 4, 6 }, "Clear only unset values ")]
        [TestCase(new[] { 2, 4, 6 }, new[] { 2, 2, 6 }, new[] { 4 }, "Clear with duplicates")]
        [TestCase(new[] { 2, 4, 6 }, new[] { 2, 2, 4, 6, 6, 6 }, new int[] {}, "Clear all preset values with duplicates")]
        public void ClearCandidates_ValidSetDistinctValues_CellChangedAndCandidatesCleared(
            IEnumerable<int> presetCandidates, IEnumerable<int> candidatesToClear, IEnumerable<int> expectedCandidates, string testCaseDescription)
        {
            // arrange
            _basicCell.SetCandidates(presetCandidates);
            Assume.That(_basicCell.Candidates.Count() == presetCandidates.Count());
            bool expectedCellChanged = !Enumerable.SequenceEqual(presetCandidates, expectedCandidates);

            // act
            bool cellChanged = _basicCell.ClearCandidates(candidatesToClear);

            // assert
            Assert.AreEqual(expectedCellChanged, cellChanged, $"Unexpected value of {nameof(cellChanged)} for test case '{testCaseDescription}'");
            CollectionAssert.AreEquivalent(expectedCandidates, _basicCell.Candidates,
                $"Final candidates not equivalent to expected for test case '{testCaseDescription}'");
        }

        [TestCase(new[] { 0, 1 })]
        [TestCase(new[] { 1, 0 })]
        [TestCase(new[] { 9, 10 })]
        [TestCase(new[] { 10, 9 })]
        public void ClearCandidates_ValidAndInvalidCandidates_ThrowsException(IEnumerable<int> validAndInvalidCandidates)
        {
            // arrange

            // act
            TestDelegate act = () => _basicCell.ClearCandidates(validAndInvalidCandidates);

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(act);

        }


        // Method: ClearAllCandidates
        [Test]
        public void ClearAllCandidates_NoSetCandidates_CellUnchanged()
        {
            // arrange
            Assume.That(_basicCell.Candidates.Count() == 0);

            // act
            bool cellChanged = _basicCell.ClearAllCandidates();

            // assert
            Assert.IsFalse(cellChanged);
            Assert.AreEqual(0, _basicCell.Candidates.Count());
        }

        [Test]
        public void ClearAllCandidates_SetCandidates_CellChangedAndCandidatesRemoved()
        {
            // arrange
            IEnumerable<int> arbitrayPreexistingCandidates = new[]
            {
                2, 6, 8
            };
            _basicCell.SetCandidates(arbitrayPreexistingCandidates);

            // act
            bool cellChanged = _basicCell.ClearAllCandidates();

            // assert
            Assert.IsTrue(cellChanged);
            Assert.IsTrue(_basicCell.Candidates.Count() == 0);
        }
    }
}
