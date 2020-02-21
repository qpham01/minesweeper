using System;

namespace MineSweeperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowCount = GetInteger("Enter number of rows: ");
            var columnCount = GetInteger("Enter number of columns: ");
            var percentMine = GetInteger("Enter the mine percentage: ", 5, 50);
            var map = new int[rowCount, columnCount];
            var mineSweeper = new MineSweeper.MineSweeper();
            mineSweeper.PopulateMines(map, percentMine / 100.0);
            var playerMap = new char[rowCount, columnCount];
            mineSweeper.InitializeBlank(playerMap);
            RunGame(map);
        }

        private static void RunGame(int[,] map)
        {
            var view = new MineSweeperConsoleView();
        }

        private static int GetInteger(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int result;
            bool success;
            do
            {
                Console.Write(prompt);
                var entry = Console.ReadLine();
                success = !int.TryParse(entry, out result);
                if (!success)
                {
                    Console.WriteLine($"{entry} is not a number.  Try again.");
                    continue;
                }

                if (result >= min && result <= max) continue;
                Console.WriteLine($"{entry} must be between {min} and {max} inclusively");
                success = false;
            } while (!success);

            return result;
        }
    }
}
