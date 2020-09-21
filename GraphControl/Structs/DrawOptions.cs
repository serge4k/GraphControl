namespace GraphControl.Structs
{
    public struct DrawOptions
    {
        public Size CanvasSize;

        public bool FitToX;

        public bool FitToY;

        public DrawOptions(Size canvasSize) : this(canvasSize, false, false)
        {
        }

        public DrawOptions(Size canvasSize, bool fitToX, bool fitToY)
        {
            this.CanvasSize = canvasSize;
            this.FitToX = fitToX;
            this.FitToY = fitToY;
        }

        public DrawOptions(DrawOptions options)
        {
            this.CanvasSize = options.CanvasSize;
            this.FitToX = options.FitToX;
            this.FitToY = options.FitToY;
        }
    }
}
