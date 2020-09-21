namespace GraphControl.Structs
{
    public struct Rect
    {
        public int Top { get; set; }

        public int Left { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Rect(int top, int left, int width, int height)
        {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
        }
    }
}
