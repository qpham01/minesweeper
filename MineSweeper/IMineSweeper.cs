using System;

namespace MineSweeper
{
    public interface IMineSweeper
    {
        int CountAdjacentMines(int[,] mineMap, int row, int column);
        void PopulateMines(int[,] mineMap, int mineCount);
        void ExploreSpace(int[,] mineMap, char[,] playerMap, int row, int column);
        void InitializeBlank(char[,] playerMap);
    }
}
