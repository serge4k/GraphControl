using System;

namespace GraphControl.Structs
{
    public struct DrawOptions : IEquatable<DrawOptions>
    {
        public Size CanvasSize { get; private set; }

        public bool FitToX { get; private set; }

        public bool FitToY { get; private set; }

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

        public override int GetHashCode()
        {
            return this.CanvasSize.GetHashCode() ^ 137
                + this.FitToX.GetHashCode() ^ 137
                + this.FitToY.GetHashCode() ^ 137;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DrawOptions))
                return false;

            return Equals((DrawOptions)obj);
        }

        public bool Equals(DrawOptions other)
        {
            return this.CanvasSize.Equals(other.CanvasSize)
                && this.FitToX.Equals(other.FitToX)
                && this.FitToY.Equals(other.FitToY);
        }

        public static bool operator ==(DrawOptions options1, DrawOptions options2)
        {
            return options1.Equals(options2);
        }

        public static bool operator !=(DrawOptions options1, DrawOptions options2)
        {
            return !options1.Equals(options2);
        }
    }
}
