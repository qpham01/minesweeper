using System;
using System.Collections.Generic;

namespace ConsoleView
{
    public class ConsoleGridView : IConsoleGridView
    {
        public void RenderMap(char[,] playerMap, int markRow, int markColumn)
        {
            var rowCount = playerMap.GetLength(0);
            var columnCount = playerMap.GetLength(1);
            Console.Write("   ");
            for (var column = 0; column < columnCount; ++column)
            {
                Console.Write($"{column,2} ");
            }
            Console.WriteLine("");
            for (var row = 0; row < rowCount; row++)
            {
                Console.Write($"{row,2} ");
                for (var column = 0; column < columnCount; column++)
                {
                    var mark = row == markRow && column == markColumn ? '*' : ' ';
                    Console.Write($"{mark}{playerMap[row, column]}{mark}");
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
