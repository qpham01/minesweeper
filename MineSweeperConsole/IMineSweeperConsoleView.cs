using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeperConsole
{
    interface IMineSweeperConsoleView
    {
        void RenderMap(char[,] playerMap, int markRow, int markColumn);
    }
}
