using System;
using System.Collections.Generic;
using System.Linq;
using MineSweeper;

namespace MineSweeperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowCount = GetIntegers("Enter number of rows", 1, 10, 40).First();
            var columnCount = GetIntegers("Enter number of columns", 1, 10, 40).First();
            var spaceCount = rowCount * columnCount;
            var minMine = (int) (0.05 * spaceCount);
            var maxMine = (int) (0.25 * spaceCount);
            var mineCount = GetIntegers("Enter the mine count", 1, minMine, maxMine).First();
            var map = new int[rowCount, columnCount];
            var mineSweeper = new MineSweeper.MineSweeper();
            mineSweeper.PopulateMines(map, mineCount);
            var playerMap = new char[rowCount, columnCount];
            mineSweeper.InitializeBlank(playerMap);
            RunGame(mineSweeper, map, playerMap);
        }

        private static void RunGame(IMineSweeper mineSweeper, int[,] mineMap, char[,] playerMap)
        {
            var view = new MineSweeperConsoleView();
            var rowCount = mineMap.GetLength(0);
            var columnCount = mineMap.GetLength(1);
            var row = -1;
            var column = -1;
            try
            {
                while (true)
                {
                    view.RenderMap(playerMap, row, column);
                    row = GetIntegers("Enter row to explore", 1, 0, rowCount - 1).First();
                    column = GetIntegers("Enter column to explore", 1, 0, columnCount - 1).First();
                    mineSweeper.ExploreSpace(mineMap, playerMap, row, column);
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Mine hit! Game over!");
                mineSweeper.ShowAllMines(mineMap, playerMap);
                view.RenderMap(playerMap, row, column);

            }
        }

        private static int[] GetIntegers(string prompt, int numberCount, int min = int.MinValue, int max = int.MaxValue)
        {
            var results = new List<int>();
            while (true)
            {
                Console.Write($"{prompt} ({min} - {max}): ");
                var entry = Console.ReadLine();
                var numbers = entry.Split(new char[] {' ', ','});
                try
                {
                    foreach (var number in numbers)
                    {
                        var value = int.Parse(number);
                        if (value < min || value > max) throw new ArgumentException();
                        results.Add(value);
                    }

                    break;
                }
                catch
                {
                    Console.WriteLine($"{entry} is not {numberCount} comma-separated integers between {min} and {max}.  Try again.");
                    continue;
                }
            } 

            return results.ToArray();
        }
    }
}
