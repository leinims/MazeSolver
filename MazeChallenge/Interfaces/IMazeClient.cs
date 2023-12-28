using MazeSolver.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Interfaces
{
    public interface IMazeClient
    {
        public Task<Result<Guid>> NewRandomMaze(int width, int height);
        public Task<Result<Guid>> NewGame(Guid mazeUid);
        public Task<Result<Block>> Move(Guid mazeUid, Guid gameUid, string operation);
        public Task<Result<Block>> GetGame(Guid mazeUid, Guid gameUid);

    }
}
