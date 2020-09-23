using GraphControl.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphControl.Tests.Unitilies
{
    internal class TestDrawingWrapper : IDrawing, IDisposable
    {
        public TimeSpan DrawDelay { get; private set; }

        public List<string> Circles { get; set; }

        public List<string> FillRectangles { get; set; }

        public List<string> Lines { get; set; }

        public List<string> MeasureTexts { get; set; }

        public List<string> Rectangles { get; set; }

        public List<string> Texts { get; set; }

        public List<string> Flushes { get; set; }

        public TestDrawingWrapper() : this(TimeSpan.TicksPerMillisecond / 100)
        {
        }

        public TestDrawingWrapper(long drawDelayTicks)
        {
            this.DrawDelay = new TimeSpan(drawDelayTicks);
            this.Circles = new List<string>();
            this.FillRectangles = new List<string>();
            this.Lines = new List<string>();
            this.MeasureTexts = new List<string>();
            this.Rectangles = new List<string>();
            this.Texts = new List<string>();
            this.Flushes = new List<string>();
        }

        public void Circle(Color color, double x, double y, double radius)
        {
            this.Circles.Add($"Circle({color.ToString()}, {x.ToString()}, {y.ToString()}, {radius.ToString()})");
            Thread.Sleep(this.DrawDelay);
        }

        public void Circle(Color color, double x, double y, double radius, RectangleF clipRectangle)
        {
            this.Circles.Add($"Circle({color.ToString()}, {x.ToString()}, {y.ToString()}, {radius.ToString()}, {clipRectangle.ToString()})");
            Thread.Sleep(this.DrawDelay);
        }

        public void FillRectangle(Color color, Color colorFill, double x, double y, double width, double height)
        {
            this.FillRectangles.Add($"FillRectangle({color.ToString()}, {colorFill.ToString()}, {x.ToString()}, {y.ToString()}, {width.ToString()}, {height.ToString()})");
            Thread.Sleep(this.DrawDelay);
        }

        public void Flush()
        {
            this.Flushes.Add($"Flush()");
        }

        public void Line(Color color, double x1, double y1, double x2, double y2)
        {
            this.Lines.Add($"Line({color.ToString()}, {x1.ToString()}, {y1.ToString()}, {x2.ToString()}, {y2.ToString()})");
            Thread.Sleep(this.DrawDelay);
        }

        public void Line(Color color, double x1, double y1, double x2, double y2, RectangleF clipRectangle)
        {
            this.Lines.Add($"Line({color.ToString()}, {x1.ToString()}, {y1.ToString()}, {x2.ToString()}, {y2.ToString()}, {clipRectangle.ToString()})");
            Thread.Sleep(this.DrawDelay);
        }

        public SizeF MeasureText(string text)
        {
            this.MeasureTexts.Add($"MeasureText({text.ToString()})");
            return new SizeF(100, 100);
        }

        public void Rectangle(Color color, double x, double y, double width, double height)
        {
            this.Rectangles.Add($"Rectangle({color.ToString()}, {x.ToString()}, {y.ToString()}, {width.ToString()}, {height.ToString()})");
            Thread.Sleep(this.DrawDelay);
        }

        public void Rectangle(Color color, double x, double y, double width, double height, RectangleF clipRectangle)
        {
            this.Rectangles.Add($"Rectangle({color.ToString()}, {x.ToString()}, {y.ToString()}, {width.ToString()}, {height.ToString()}, {clipRectangle.ToString()})");
            Thread.Sleep(this.DrawDelay);
        }

        public void Text(Color color, double x, double y, string value)
        {
            this.Texts.Add($"Circle({color.ToString()}, {x.ToString()}, {y.ToString()}, {value.ToString()})");
            Thread.Sleep(this.DrawDelay);
        }

        public void Text(Color color, Rectangle rect, string value, StringAlignment alignment, StringAlignment lineAlignment)
        {
            this.Texts.Add($"Circle({color.ToString()}, {rect.ToString()}, {value}, {alignment.ToString()}, {lineAlignment.ToString()})");
            Thread.Sleep(this.DrawDelay);
        }

        public void Dispose()
        {
            Flush();
        }
    }
}
