using System;
using System.Collections.Generic;
using System.Linq;
using MineSweeper;

namespace MineSweeperConsole
{
    class Program
    {
        private static IMineSweeperConsoleView _view;

        static void Main(string[] args)
        {
            _view = new MineSweeperConsoleView();
            var rowCount = _view.GetIntegers("Enter number of rows", 1, 10, 40).First();
            var columnCount = _view.GetIntegers("Enter number of columns", 1, 10, 40).First();
            var spaceCount = rowCount * columnCount;
            var minMine = (int) (0.05 * spaceCount);
            var maxMine = (int) (0.25 * spaceCount);
            var mineCount = _view.GetIntegers("Enter the mine count", 1, minMine, maxMine).First();
            var map = new int[rowCount, columnCount];
            var mineSweeper = new MineSweeper.MineSweeper();
            mineSweeper.PopulateMines(map, mineCount);
            var playerMap = new char[rowCount, columnCount];
            mineSweeper.InitializeBlank(playerMap);
            RunGame(mineSweeper, map, playerMap);
        }

        private static void RunGame(IMineSweeper mineSweeper, int[,] mineMap, char[,] playerMap)
        {
            var rowCount = mineMap.GetLength(0);
            var columnCount = mineMap.GetLength(1);
            var row = -1;
            var column = -1;
            try
            {
                while (true)
                {
                    _view.RenderMap(playerMap, row, column);
                    row = _view.GetIntegers("Enter row to explore", 1, 0, rowCount - 1).First();
                    column = _view.GetIntegers("Enter column to explore", 1, 0, columnCount - 1).First();
                    mineSweeper.ExploreSpace(mineMap, playerMap, row, column);
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Mine hit! Game over!");
                mineSweeper.ShowAllMines(mineMap, playerMap);
                _view.RenderMap(playerMap, row, column);

            }
        }
    }
}
