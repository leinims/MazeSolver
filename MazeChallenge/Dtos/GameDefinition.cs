using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Dtos
{
    public class GameDefinition
    {
        public Guid MazeUid { get; set; }
        public Guid GameUid { get; set; }
        public bool Completed { get; set; }
        public int CurrentPositionX { get; set; }
        public int CurrentPositionY { get; set; }
        
    }
}
