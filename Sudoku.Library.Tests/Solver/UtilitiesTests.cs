using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sudoku.Library.Solver;

namespace Sudoku.Library.Tests.Solver
{
    [TestFixture]
    public class UtilitiesTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(255, 8)]
        [TestCase(65535, 16)]
        [TestCase(-1, 32)]
        public void CountSetBits_Various_ReturnsExpectedCount(int testValue, int expectedCount)
        {
            // arrange
            var utils = new Utilities();

            // act
            int actualCount = utils.CountSetBits(testValue);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(4, 24)]
        [TestCase(5, 120)]
        [TestCase(6, 720)]
        [TestCase(7, 5040)]
        [TestCase(8, 40320)]
        public void Factorial_ValidValues_ReturnsExpectedValue(int testValue, int expectedResult)
        {
            // arrange
            var utils = new Utilities();

            // act
            int actualResult = utils.Factorial(testValue);

            // assert 
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(-1)]
        [TestCase(15)]
        public void Factorial_InvalidValues_Throws(int testValue)
        {
            // arrange
            var utils = new Utilities();

            // act
            TestDelegate act = () => utils.Factorial(testValue);

            // assert 
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Test]
        public void GetCombinations_Choose3Of6_ReturnsOptions()
        {
            // arrange
            var utils = new Utilities();

            int[] arr = { 1, 2, 3, 4, 5, 6 };
            int r = 3;

            int expectedNumberOfCombinations = Factorial(arr.Length) / (Factorial(r) * Factorial(arr.Length - r));


            // act
            List<HashSet<int>> combinations = utils.GetCombinations(arr, r);

            // assert
            Assert.IsNotNull(combinations);

            Assert.AreEqual(expectedNumberOfCombinations, combinations.Count);

            // all combinations are of size r
            foreach (var set in combinations)
                Assert.AreEqual(r, set.Count);

            // combinations are unique
            var testSet = new HashSet<string>();
            foreach (var set in combinations)
            {
                var key = string.Join("|", set);
                Assert.IsTrue(testSet.Add(key));
            }
        }

        private int Factorial(int n)
        {
            if (n < 0 || n > 14)
                throw new IndexOutOfRangeException();

            if (n == 0 || n == 1)
                return 1;

            int answer = 1;

            for (int i = n; i > 1; i--)
                answer *= i;

            return answer;
        }
    }
}
