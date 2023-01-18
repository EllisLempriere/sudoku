using NUnit.Framework;

namespace Sudoku.ConsoleApp.Tests
{
    public class GridConsoleViewTests
    {
        [SetUp]
        public void Setup()
        {
            // nothing yet
        }

        [Test]
        public void Constructor_Default_ReturnsInstance()
        {
            // arrange

            // act
            var view = new GridConsoleView();

            // assert
            Assert.IsNotNull(view);
        }
    }
}