using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;
using System.Drawing;

namespace GraphControl.Core.Views
{
    public class ScalingSelectionView : IScalingSelectionView
    {
        public Point? MovingStart { get; set; }

        public Point? MovingPosition { get; set; }

        public Point? ScalingStart { get; set; }

        public Point? ScalingPosition { get; set; }

        public bool ZoomIncrease { get; set; }

        public Color MovingPenColor { get; set; }

        public Color ZoomInPenColor { get; set; }

        public Color ZoomOutPenColor { get; set; }

        public ScalingSelectionView()
        {
        }

        public virtual void Draw(IDrawing drawing, DrawOptions options, IMargin margin)
        {
            if (drawing == null || margin == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            var canvasSize = options.CanvasSize;
            var clip = new System.Drawing.RectangleF((float)margin.Left, (float)margin.Top, (float)(canvasSize.Width - margin.LeftAndRight), (float)(canvasSize.Height - margin.TopAndBottom));

            if (this.MovingStart != null && this.MovingPosition != null)
            {
                drawing.Line(this.MovingPenColor, this.MovingStart.Value.X, this.MovingStart.Value.Y, this.MovingPosition.Value.X, this.MovingPosition.Value.Y, clip);
            }

            if (this.ScalingStart != null && this.ScalingPosition != null)
            {
                Rectangle rectangle = SortCoordinates(this.ScalingStart.Value, this.ScalingPosition.Value);
                var color = this.ZoomIncrease ? this.ZoomInPenColor : this.ZoomOutPenColor;
                drawing.Rectangle(color, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, clip);
            }
        }

        private static Rectangle SortCoordinates(Point scalingStart, Point scalingPos)
        {
            var x1 = scalingStart.X;
            var x2 = scalingPos.X;
            var y1 = scalingStart.Y;
            var y2 = scalingPos.Y;
            if (x1 > x2)
            {
                x2 = scalingStart.X;
                x1 = scalingPos.X;
            }
            if (y1 > y2)
            {
                y2 = scalingStart.Y;
                y1 = scalingPos.Y;
            }
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}
