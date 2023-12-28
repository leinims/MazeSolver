using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver.Dtos
{
    public class GameRequest
    {
        public GameRequest(string Operation)
        {
            this.Operation = Operation;
        }
        public string Operation {  get; set; }
    }
}
