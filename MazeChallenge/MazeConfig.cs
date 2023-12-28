using System;

namespace MazeSolver
{
    public class MazeConfig
    {
        public APISettings APISettings { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<string> Directions { get; set; }

    }
    public class APISettings
    {
        public string APIname { get; set; }
        public string URI { get; set; }
        
        public string AccessCode { get; set; }

    }

}
