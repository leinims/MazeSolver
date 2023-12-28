using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Entities
{
    public class Maze
    {
        public Guid MazeUid { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Maze(Guid mazeUid, int width, int height)
        {
            MazeUid = mazeUid;
            Width = width;
            Height = height;
        }
    }
}
