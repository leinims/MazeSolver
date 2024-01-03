using MazeSolver.Entities;
using MazeSolver.Interfaces;
using MazeSolver.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.PathFinder
{
    public class RightHand : IPathFinder
    {

        public RightHand(Vector2Int beginPos, Vector2Int endPos)
        {
            
        }
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
