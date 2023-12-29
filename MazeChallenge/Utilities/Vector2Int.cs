using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Utilities
{
    public struct Vector2Int
    {
        public int X;
        public int Y;
        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static Vector2Int operator +(Vector2Int a, Vector2Int b) =>
            new Vector2Int(a.X + b.X, a.Y + b.Y);    
        
    }
}
