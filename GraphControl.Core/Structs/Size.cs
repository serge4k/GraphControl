using System;

namespace GraphControl.Core.Structs
{
    public struct Size : IEquatable<Size>
    {
        public int Width { get;  set; }

        public int Height { get; set; }

        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public override int GetHashCode()
        {
            return this.Width.GetHashCode() ^ 137
                + this.Height.GetHashCode() ^ 137;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Size))
                return false;

            return Equals((Size)obj);
        }

        public bool Equals(Size other)
        {
            return this.Width.Equals(other.Width)
                && this.Height.Equals(other.Height);
        }

        public static bool operator ==(Size size1, Size size2)
        {
            return size1.Equals(size2);
        }

        public static bool operator !=(Size size1, Size size2)
        {
            return !size1.Equals(size2);
        }
    }
}
