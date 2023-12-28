using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Interfaces
{
    public interface IMazeService
    {
        public Task<bool> CreateMaze();
        public Task<bool> SolveMaze();
    }
}
