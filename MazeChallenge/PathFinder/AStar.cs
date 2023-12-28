using MazeSolver.Entities;
using MazeSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.PathFinder
{
    public class AStar : IPathFinder
    {
        private List<Node> nodes;
        private Vector2Int beginPos;
        private Vector2Int endPos;
        private Node currentNode;

        public AStar(Vector2Int beginPos, Vector2Int endPos)
        {
            this.beginPos = beginPos;
            this.endPos = endPos;
            
            nodes = new List<Node>();
            currentNode = new Node(null, beginPos, 0, getManhattanDistance(beginPos));
            nodes.Add(currentNode);
        }
        public bool FindNextPosition(ref Game game)
        {
            
            for (int i = 0; i < game.CurrentBlock.Neighbours.Count ; i++)
            {
                if (!game.CurrentBlock.Neighbours[i])
                {
                    Vector2Int position = new Vector2Int();
                    position.X = currentNode.Position.X + (game.CurrentBlock.Neighbours[i]? 1 : 0) * (i % 2 -1);
                    position.Y = currentNode.Position.Y;
                    nodes.Add( new Node(currentNode, new Vector2Int(), 0, 0));
                }

            }

            return true;
        }
        private int getManhattanDistance(Vector2Int position)
        {
            return Math.Abs(endPos.X - position.X) + Math.Abs(endPos.Y - position.Y);
        }
    }
}
