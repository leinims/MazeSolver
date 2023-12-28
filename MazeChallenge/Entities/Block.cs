using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Entities
{
    public class Block
    {
        public Block(int posX, int posY, List<bool> neighbours, bool isLastBlock)
        {
            PosX = posX;
            PosY = posY;
            Neighbours = neighbours;
            IsLastBlock = isLastBlock;

        }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public List<bool> Neighbours { get; set; }
        public bool IsLastBlock { get; set; }
        
    }
}
