using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphControl.Core.Structs
{
    public struct DrawingPathItem : IEquatable<DrawingPathItem>, IDisposable
    {
        public GraphicsPath Path { get; set; }

        public Brush Brush { get; set; }

        public Pen Pen { get; set; }

        public RectangleF? Clip { get; set; }

        public DrawingPathItem(GraphicsPath path, Brush brush, Pen pen) : this(path, brush, pen, null)
        {
        }

        public DrawingPathItem(GraphicsPath path, Brush brush, Pen pen, RectangleF? clip)
        {
            this.Path = path;
            this.Brush = brush;
            this.Pen = pen;
            this.Clip = clip;
        }

        public override int GetHashCode()
        {
            return this.Path.GetHashCode() ^ 2147483647
                + this.Brush.GetHashCode() ^ 2147483647
                + this.Pen.GetHashCode() ^ 2147483647
                + (this.Clip != null ? this.Clip.GetHashCode() ^ 2147483647 : 0);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DrawingPathItem))
                return false;

            return Equals((DrawingPathItem)obj);
        }

        public bool Equals(DrawingPathItem other)
        {
            return this.Path.Equals(other.Path)
                && this.Brush.Equals(other.Brush)
                && this.Pen.Equals(other.Pen)
                && this.Clip != null && this.Clip.Equals(other.Clip);
        }

        public static bool operator ==(DrawingPathItem path1, DrawingPathItem path2)
        {
            return path1.Equals(path2);
        }

        public static bool operator !=(DrawingPathItem path1, DrawingPathItem path2)
        {
            return !path1.Equals(path2);
        }

        public void Dispose()
        {
            this.Path?.Dispose();
            this.Brush?.Dispose();
            this.Pen?.Dispose();
        }
    }
}
