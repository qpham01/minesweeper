using System;

namespace MineSweeper
{
    public interface IMineSweeper
    {
        int CountAdjacentMines(int[,] mineMap, int row, int column);
        int PopulateMines(int[,] mineMap, double probability);
        void ExploreSpace(int[,] mineMap, char[,] playerMap, int row, int column);
        void InitializeBlank(char[,] playerMap);
    }
}
