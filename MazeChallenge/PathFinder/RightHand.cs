using MazeSolver.Entities;
using MazeSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.PathFinder
{
    public class RightHand : IPathFinder
    {
        public bool FindNextPosition(ref Game game)
        {
            game.Direction += 1;
            while (!game.CurrentBlock.Neighbours[game.Direction])
            {
                game.Direction -= 1;
            }
            return true;
        }
    }
}
