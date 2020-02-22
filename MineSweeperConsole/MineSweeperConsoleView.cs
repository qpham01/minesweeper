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
    }
}
