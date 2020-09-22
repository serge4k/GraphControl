using System;

namespace GraphControlCore.Structs
{
    public struct Rect : IEquatable<Rect>
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

        public override int GetHashCode()
        {
            return this.Top.GetHashCode() ^ 137
                + this.Left.GetHashCode() ^ 137
                + this.Width.GetHashCode() ^ 137
                + this.Height.GetHashCode() ^ 137;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Size))
                return false;

            return Equals((Size)obj);
        }

        public bool Equals(Rect other)
        {
            return this.Top.Equals(other.Top)
                && this.Left.Equals(other.Left)
                && this.Width.Equals(other.Width)
                && this.Width.Equals(other.Width);
        }

        public static bool operator ==(Rect rect1, Rect rect2)
        {
            return rect1.Equals(rect2);
        }

        public static bool operator !=(Rect rect1, Rect rect2)
        {
            return !rect1.Equals(rect2);
        }
    }
}
