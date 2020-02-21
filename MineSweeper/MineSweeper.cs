using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    public class MineSweeper : IMineSweeper
    {
        public const char Unexplored = '.';

        public int CountAdjacentMines(int[,] mineMap, int row, int column)
        {
            if (mineMap[row, column] == 1) return -1;
            var rowCount = mineMap.GetLength(0);
            var columnCount = mineMap.GetLength(1);
            var mineCount = 0;
            for (var iRow = row - 1; iRow <= row + 1; ++iRow)
            {
                if (iRow < 0) continue;
                if (iRow >= rowCount) continue;
                
                for (var iColumn = column - 1; iColumn <= column + 1; ++iColumn)
                {
                    if (iColumn < 0) continue;
                    if (iColumn >= columnCount) continue;
                    mineCount += mineMap[iRow, iColumn];
                }
            }

            return mineCount;
        }

        public int PopulateMines(int[,] mineMap, double probability)
        {
            var rand = new Random();
            var rowCount = mineMap.GetLength(0);
            var columnCount = mineMap.GetLength(1);
            var mineCount = 0;
            for (var row = 0; row < rowCount; ++row)
            {
                for (var column = 0; column < columnCount; ++column)
                {
                    mineMap[row, column] = (rand.NextDouble() < probability) ? 1 : 0;
                    mineCount += mineMap[row, column];
                }
            }

            return mineCount;
        }

        public void ExploreSpace(int[,] mineMap, char[,] playerMap, int row, int column)
        {
            var mineCount = CountAdjacentMines(mineMap, row, column);
            if (mineCount < 0)
            {
                throw new InvalidOperationException("Hit a mine!");
            }
            playerMap[row, column] = (char)('0' + mineCount);
            if (mineCount != 0) return;
            var exploreStack = new Stack<Tuple<int, int>>();
            var rowCount = mineMap.GetLength(0);
            var columnCount = mineMap.GetLength(1);

            exploreStack.Push(new Tuple<int, int>(row, column));
            while (exploreStack.Count > 0)
            {
                var space = exploreStack.Pop();
                var currentRow = space.Item1;
                var currentColumn = space.Item2;

                for (var iRow = currentRow - 1; iRow <= currentRow + 1; ++iRow)
                {
                    if (iRow < 0) continue;
                    if (iRow >= rowCount) continue;

                    for (var iColumn = currentColumn - 1; iColumn <= currentColumn + 1; ++iColumn)
                    {
                        if (iColumn < 0) continue;
                        if (iColumn >= columnCount) continue;
                        if (playerMap[iRow, iColumn] != MineSweeper.Unexplored) continue;

                        mineCount = CountAdjacentMines(mineMap, iRow, iColumn);
                        if (mineCount == 0)
                        {
                            exploreStack.Push(new Tuple<int, int>(iRow, iColumn));
                        }

                        playerMap[iRow, iColumn] = (char)('0' + mineCount);
                    }
                }
            }
        }

        public void InitializeBlank(char[,] playerMap)
        {
            var rowCount = playerMap.GetLength(0);
            var columnCount = playerMap.GetLength(1);

            for (var row = 0; row < rowCount; ++row)
            {
                for (var column = 0; column < columnCount; ++column)
                {
                    playerMap[row, column] = Unexplored;
                }
            }
        }
    }
}
