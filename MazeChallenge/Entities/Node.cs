using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Entities
{
    public class Node
    {
        public Vector2Int Position { get; set; }
        public Node Parent { get; set; }
        public Node[] Neighbours { get; set; }
        public int GScore { get; set; }
        public int HScore { get; set; }
        public bool IsClose { get; set; } = false;
        public Node(Node parent, Vector2Int position, int gScore, int hScore )
        {
            Parent = parent;
            Position = position;
            GScore = gScore;
            HScore = hScore;
        }
    }
}
