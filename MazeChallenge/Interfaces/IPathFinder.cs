using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeSolver.Entities;

namespace MazeSolver.Interfaces
{
    public interface IPathFinder
    {
        public bool FindNextPosition(ref Game game);
    }
}
