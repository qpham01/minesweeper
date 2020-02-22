using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeperConsole
{
    public class MineSweeperConsoleView : IMineSweeperConsoleView
    {
        public void RenderMap(char[,] playerMap, int markRow, int markColumn)
        {
            var rowCount = playerMap.GetLength(0);
            var columnCount = playerMap.GetLength(1);
            for (var row = 0; row < rowCount; row++)
            {
                for (var column = 0; column < columnCount; column++)
                {
                    var mark = row == markRow && column == markColumn ? '*' : ' ';
                    Console.Write($"{mark}{playerMap[row, column]}{mark}");
                }
                Console.Write(Environment.NewLine);
            }
        }

        public int[] GetIntegers(string prompt, int numberCount, int min = int.MinValue, int max = int.MaxValue)
        {
            var results = new List<int>();
            while (true)
            {
                Console.Write($"{prompt} ({min} - {max}): ");
                var entry = Console.ReadLine();
                var numbers = entry.Split(new char[] { ' ', ',' });
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
