using NUnit.Framework;

namespace MineSweeper.UnitTests
{
    public class Tests
    {
        private readonly int[,] _map1 = new int[,]
        {
            {1, 0, 1, 1, 1, 0},
            {1, 0, 0, 0, 1, 0},
            {1, 1, 0, 0, 0, 1},
            {0, 0, 0, 1, 1, 1},
            {1, 1, 1, 0, 1, 0},
        };

        private readonly int[,] _map2 = new int[,]
        {
            {1, 0, 1, 1, 1, 0},
            {1, 0, 0, 0, 0, 1},
            {1, 1, 0, 0, 0, 1},
            {1, 1, 0, 0, 0, 1},
            {0, 0, 0, 0, 0, 1},
            {0, 0, 0, 0, 1, 1},
            {0, 0, 0, 0, 1, 1},
        };

        private IMineSweeper _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new MineSweeper();
        }

        [TestCase(0, 0, -1)]
        [TestCase(1, 0, -1)]
        [TestCase(4, 4, -1)]
        [TestCase(0, 1, 3)]
        [TestCase(1, 1, 5)]
        [TestCase(2, 3, 3)]
        [TestCase(3, 0, 4)]
        [TestCase(4, 5, 3)]
        public void TestGetAdjacentMine(int x, int y, int count)
        {
            Assert.AreEqual(count, _sut.CountAdjacentMines(_map1, x, y));
        }

        [TestCase(20, 20, 40)]
        [TestCase(30, 20, 100)]
        [TestCase(30, 40, 500)]
        public void TestPopulateMines(int rowCount, int columnCount, int mineCount)
        {
            var map = new int[rowCount, columnCount];
            _sut.PopulateMines(map, mineCount);
            var mines = 0;
            for (var row = 0; row < rowCount; ++row)
            {
                for (var column = 0; column < columnCount; ++column)
                {
                    mines += map[row, column];
                }
            }
            Assert.AreEqual(mineCount, mines);
        }

        [Test]
        public void TestExploreSpace()
        {
            var rowCount = _map2.GetLength(0);
            var columnCount = _map2.GetLength(1);
            var playerMap = new char[rowCount, columnCount];
            _sut.InitializeBlank(playerMap);

            _sut.ExploreSpace(_map2, playerMap, 0,1);
            Assert.AreEqual('3', playerMap[0, 1]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[1, 1]);

            _sut.ExploreSpace(_map2, playerMap, 1, 1);
            Assert.AreEqual('3', playerMap[0, 1]);
            Assert.AreEqual('5', playerMap[1, 1]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[1, 2]);

            _sut.ExploreSpace(_map2, playerMap, 2, 2);
            Assert.AreEqual('5', playerMap[1, 1]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[1, 2]);
            Assert.AreEqual('2', playerMap[2, 2]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[2, 1]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[3, 2]);

            _sut.ExploreSpace(_map2, playerMap, 2, 3);
            Assert.AreEqual('3', playerMap[1, 2]);
            Assert.AreEqual('2', playerMap[2, 2]);
            Assert.AreEqual('3', playerMap[1, 3]);
            Assert.AreEqual('3', playerMap[1, 3]);
            Assert.AreEqual('4', playerMap[1, 4]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[1, 5]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[0, 4]);
            Assert.AreEqual('0', playerMap[3, 3]);
            Assert.AreEqual('1', playerMap[4, 2]);
            Assert.AreEqual('1', playerMap[4, 3]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[5, 1]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[5, 2]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[5, 3]);
            Assert.AreEqual(MineSweeper.Unexplored, playerMap[5, 4]);

            _sut.ExploreSpace(_map2, playerMap, 5, 1);
            Assert.AreEqual('0', playerMap[5, 0]);
            Assert.AreEqual('0', playerMap[5, 1]);
            Assert.AreEqual('0', playerMap[5, 2]);
            Assert.AreEqual('0', playerMap[6, 0]);
            Assert.AreEqual('0', playerMap[6, 1]);
            Assert.AreEqual('0', playerMap[6, 2]);
            Assert.AreEqual('2', playerMap[4, 0]);
            Assert.AreEqual('2', playerMap[4, 1]);
        }
    }
}