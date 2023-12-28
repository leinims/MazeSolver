using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Entities
{
    public class Game
    {
        public Game(Guid gameUid, Block currentBlock)
        {
            GameUid = gameUid;
            CurrentBlock = currentBlock;
        }
        public Guid GameUid {  get; set; }
        public bool isCompleted { get; set; } = false;
        public Block CurrentBlock { get; set; }
        public int Steps { get; set; }

        ////N=0,E=1,S=2,W=3
        private int _direction;
        public int Direction
        {
            get => _direction;
            set { 
                _direction = value < 0 ? value + 4 : value % 4;
            } 
        }

    }
}
