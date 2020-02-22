using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeperConsole
{
    interface IMineSweeperConsoleView
    {
        void RenderMap(char[,] playerMap, int markRow, int markColumn);
        int[] GetIntegers(string prompt, int numberCount, int min = int.MinValue, int max = int.MaxValue);
    }
}
