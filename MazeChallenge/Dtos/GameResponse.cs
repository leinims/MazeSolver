using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Dtos
{
    public class GameResponse
    {
        public GameDefinition Game {  get; set; }
        public MazeBlockView MazeBlockView { get; set; }
    }
}
