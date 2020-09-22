using System;
using System.Drawing;

namespace GraphControl.Utilities
{
    public sealed class DrawingBuffer : IDisposable
    {
        public Graphics Graphics { get; private set; }

        public Structs.Size CanvasSize { get; private set; }

        public Bitmap Bitmap { get; private set; }

        public DrawingBuffer(int width, int height)
            : this(width, height, System.Drawing.Drawing2D.SmoothingMode.AntiAlias)
        {
        }

        public DrawingBuffer(int width, int height, System.Drawing.Drawing2D.SmoothingMode smoothingMode)
            : this(new Structs.Size(width, height), smoothingMode)
        {
        }

        public DrawingBuffer(Structs.Size canvasSize) : this(canvasSize, System.Drawing.Drawing2D.SmoothingMode.AntiAlias)
        {
        }

        public DrawingBuffer(Structs.Size canvasSize, System.Drawing.Drawing2D.SmoothingMode smoothingMode)
        {
            this.Bitmap = new Bitmap(canvasSize.Width, canvasSize.Height);
            this.Graphics = Graphics.FromImage(this.Bitmap);
            this.Graphics.SmoothingMode = smoothingMode;
            this.CanvasSize = canvasSize;
        }

        public void Dispose()
        {
            this.Graphics.Dispose();
            this.Bitmap.Dispose();
        }
    }
}
