using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using GraphControl.Definitions;
using GraphControl.Interfaces;

namespace GraphControl.Utilites
{
    public sealed class Drawing2DWrapper : IDrawing, IDisposable
    {
        #region Private fields
        private Graphics graphicsCanvas;
        private FontFamily fontFamily;
        private int fontSize;
        private Font font;

        private ICollection<DrawingPathItem> cachePath;
        #endregion

        #region Constructors
        public Drawing2DWrapper(Graphics graphics)
        {
            this.graphicsCanvas = graphics;
            this.fontFamily = new FontFamily("Arial");
            this.fontSize = 12;
            this.font = new Font(this.fontFamily, this.fontSize);
            this.cachePath = new List<DrawingPathItem>();
        }
        #endregion

        #region Public methods
        public void Rectangle(Color color, double x, double y, double width, double height)
        {
            var graphics = GetGraphics();
            var path = AddPath(null, new Pen(color));
            path.AddRectangle(new RectangleF((float)x, (float)y, (float)width, (float)height));
        }
        public void Rectangle(Color color, double x, double y, double width, double height, RectangleF clipRectangle)
        {
            var graphics = GetGraphics();
            var path = AddPath(null, new Pen(color), clipRectangle);
            path.AddRectangle(new RectangleF((float)x, (float)y, (float)width, (float)height));
        }

        public void FillRectangle(Color color, Color colorFill, double x, double y, double width, double height)
        {
            var graphics = GetGraphics();
            var path = AddPath(new SolidBrush(colorFill), new Pen(color));
            path.AddRectangle(new RectangleF((float)x, (float)y, (float)width, (float)height));
        }

        public void Line(Color color, double x1, double y1, double x2, double y2)
        {
            var graphics = GetGraphics();
            var path = AddPath(null, new Pen(color));
            path.AddLine((float)x1, (float)y1, (float)x2, (float)y2);
        }

        public void Line(Color color, double x1, double y1, double x2, double y2, RectangleF clipRectangle)
        {
            var graphics = GetGraphics();
            var path = AddPath(null, new Pen(color), clipRectangle);
            path.AddLine((float)x1, (float)y1, (float)x2, (float)y2);
        }

        public void Text(Color color, double x, double y, string text)
        {
            var graphics = GetGraphics();
            var path = AddPath(new SolidBrush(color), null);
            float emSize = this.graphicsCanvas.DpiY * this.font.Size / 72;
            path.AddString(text, this.fontFamily, (int)this.font.Style, emSize, new PointF((float)x, (float)y), StringFormat.GenericDefault);
        }

        public void Text(Color color, Rectangle rect, string text, StringAlignment alignment, StringAlignment lineAlignment)
        {
            var graphics = GetGraphics();
            var path = AddPath(new SolidBrush(color), null);
            float emSize = this.graphicsCanvas.DpiY * this.font.Size / 72;
            StringFormat stringFormat = new StringFormat(StringFormatFlags.NoClip);
            stringFormat.Alignment = alignment;
            stringFormat.LineAlignment = lineAlignment;
            path.AddString(text, this.fontFamily, (int)this.font.Style, emSize, rect, stringFormat);
        }

        public void Circle(Color color, double x, double y, int radius)
        {
            var graphics = GetGraphics();
            var path = AddPath(new SolidBrush(color), null);
            path.AddEllipse((float)(x - radius/2), (float)(y - radius / 2), (float)(radius), (float)(radius));
        }

        public SizeF MeasureText(string text)
        {
            var graphics = GetGraphics();
            return graphics.MeasureString(text, this.font);
        }

        public void Flush()
        {
            if (this.cachePath != null)
            {
                foreach (var item in this.cachePath)
                {
                    // Set clip when not equals and store old
                    RectangleF oldClip = new RectangleF();
                    if (item.Clip != null && !graphicsCanvas.ClipBounds.Equals(item.Clip.Value))
                    {
                        oldClip = graphicsCanvas.ClipBounds;
                        graphicsCanvas.SetClip(item.Clip.Value);
                    }
                    
                    if (item.Brush != null)
                    {
                        GetGraphics().FillPath(item.Brush, item.Path);
                    }
                    if (item.Pen != null)
                    {
                        GetGraphics().DrawPath(item.Pen, item.Path);
                    }

                    // Restore clip when not equals
                    if (item.Clip != null && !graphicsCanvas.ClipBounds.Equals(oldClip))
                    {
                        graphicsCanvas.SetClip(oldClip);
                    }

                    item.Path.Dispose();
                }
                
                this.cachePath.Clear();
                this.cachePath = null;
            }
        }

        public void Dispose()
        {
            this.fontFamily.Dispose();
            this.font.Dispose();
            Flush();
        }
        #endregion

        #region Private methods
        private GraphicsPath AddPath(Brush brush, Pen pen, RectangleF? clipRectangle = null)
        {
            if (this.cachePath != null)
            {
                var path = new GraphicsPath();
                var item = new DrawingPathItem(path, brush, pen, clipRectangle);
                this.cachePath.Add(item);
                return path;
            }
            else
            {
                return new GraphicsPath();
            }
        }
        
        private Graphics GetGraphics()
        {
            return this.graphicsCanvas;   
        }
        #endregion
    }
}
