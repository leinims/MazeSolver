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
            currentNode.Path.Add(currentNode);
            nodes.Add(currentNode);

        }
        public bool FindNextPosition(ref Game game)
        {
            
            if(game.CurrentBlock.Neighbours.Where(e => true).Count() > 1 && currentNode.numOfClosedNeigh < 3 && !currentNode.WasAnalyzed) {
                currentNode.WasAnalyzed = true;
                for (int i = 0; i < game.CurrentBlock.Neighbours.Count ; i++)
                {
                    //Check if exists this node
                    
                    //Looking for new nodes
                    if (game.CurrentBlock.Neighbours[i])
                    {
                        //Avoid parent Path
                        if (Math.Abs(game.Direction - i) == 2 && currentNode.Parent != null) continue;
                        Vector2Int position = new Vector2Int();
                        //North: 0-1, South 2-1, East: 2-1, West: 2-3
                        position.X = currentNode.Position.X + (game.CurrentBlock.Neighbours[i] && i % 2 == 1 ? 2 - i : 0);
                        position.Y = currentNode.Position.Y + (game.CurrentBlock.Neighbours[i] && i % 2 == 0 ? i - 1 : 0);

                        if (isThereNode(position)) continue;

                        Node node = new Node(currentNode, position, game.Steps + 1, getManhattanDistance(position));
                        
                        //Adding path 
                        foreach (Node n in currentNode.Path)
                        {
                            node.Path.Add(n);
                        }
                        node.Path.Add(node);
                        nodes.Add(node);
                    }
                    else
                    {
                        currentNode.numOfClosedNeigh++;
                    }
                }
            }
            if(currentNode.numOfClosedNeigh > 2 && currentNode.Parent != null)
            {
                currentNode.IsClose = true;

                 currentNode.Parent.numOfClosedNeigh++;
                //return to parent position
                game.Direction = goNextNode(currentNode, currentNode.Parent);
                currentNode = currentNode.Parent;
                return true;
            }

            //Search lower Cost node //use for Reverse
            Node choosedNode = nodes[0];
            foreach (var node in nodes)
            {
                if (node.HScore < choosedNode.HScore && !node.IsClose 
                    && node != currentNode && currentNode == node.Parent) //&& node != currentNode.Parent)
                    choosedNode = node;
            }
            //Check last commo node between current an lower cost node
            if(currentNode == choosedNode.Parent)
            {

                game.Direction = goNextNode(currentNode, choosedNode);
                currentNode = choosedNode;
                return true;
            }
            if (choosedNode.Path.Contains(currentNode))
            {
                int i = 0;
                for (i = 0; i<choosedNode.Path.Count; i++)
                {
                    if (choosedNode.Path[i] == currentNode)
                        break;
                }
                game.Direction = goNextNode(currentNode, choosedNode.Path[i+1]);
                currentNode = choosedNode.Path[i + 1];
                return true;
            }
            else
            {
                game.Direction = goNextNode(currentNode, currentNode.Parent);
                currentNode = currentNode.Parent;
                return true;
            }
            
          
            return false;
        }
        private int getManhattanDistance(Vector2Int position)
        {
            return Math.Abs(endPos.X - position.X) + Math.Abs(endPos.Y - position.Y);
        }
        private int goNextNode(Node fromNode, Node toNode)
        {
            int dX = fromNode.Position.X- toNode.Position.X;
            int dY = toNode.Position.Y - fromNode.Position.Y;
            int newDirection = Math.Abs(dX * (dX + 2) + dY * (dY + 1));
            return newDirection;
        }
        private bool isThereNode(Vector2Int position)
        {
            foreach (var node in nodes)
            {
                if (node.Position.X == position.X && node.Position.Y == position.Y) return true;
            }
            return false;
        }
    }
}
