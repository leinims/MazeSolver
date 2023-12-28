namespace MazeSolver.Dtos
{
    public class MazeRequest
    {
        public MazeRequest(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
        public int Width { get; set; }
        public int Height { get; set; }

    }
}
