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
            const string commandPrompt = "Enter space to explore or flag";
            const string expectedInput = "e or f,row,column";
            try
            {
                while (true)
                {
                    _view.RenderMap(playerMap, row, column);
                    string command;
                    while (true)
                    {
                        var inputs = _view.GetLineTokens(commandPrompt, expectedInput, 3);
                        if ((inputs[0] != "e" && inputs[0] != "f") ||
                            !int.TryParse(inputs[1], out row) ||
                            !int.TryParse(inputs[2], out column))
                        {
                            Console.WriteLine($"{inputs[0]},{inputs[1]},{inputs[2]} is not {expectedInput}");
                            continue;
                        }

                        command = inputs[0];
                        break;
                    }

                    if (command == "e")
                    {
                        mineSweeper.ExploreSpace(mineMap, playerMap, row, column);
                    }

                    if (command == "f")
                    {
                        playerMap[row, column] = 'F';
                    }
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
