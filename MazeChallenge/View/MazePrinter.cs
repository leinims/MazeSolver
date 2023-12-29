using MazeSolver.Entities;
using MazeSolver.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.View
{

    internal class MazePrinter
    {
        private string[,] mazeMatrix;
        private Vector2Int position;
        public MazePrinter(Vector2Int size) {
            mazeMatrix = new string[size.X, size.Y];

            for (int i = 0; i < mazeMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < mazeMatrix.GetLength(1); j++)
                {
                    mazeMatrix[i, j] = " ";
                }
            }
        }
        public void UpdateScreen(Game game)
        {
            mazeMatrix[position.X, position.Y] = "x";
            position.X = game.CurrentBlock.PosX;
            position.Y = game.CurrentBlock.PosY;
            mazeMatrix[position.X, position.Y] = "@";
            Console.Clear();
            for (int i = 0; i < mazeMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < mazeMatrix.GetLength(1); j++)
                {
                    Console.Write(mazeMatrix[j, i] +  " ");
                }
                Console.WriteLine();
            }

        }
    }
}
