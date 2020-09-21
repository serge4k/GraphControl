using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphControl.Definitions
{
    public struct DrawingPathItem
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
    }
}
