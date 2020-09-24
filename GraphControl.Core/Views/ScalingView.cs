using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Views;
using System.Drawing;

namespace GraphControl.Core.Views
{
    public class ScalingView : IScalingSelectionView
    {
        #region Public properties

        #endregion

        #region Constructors
        public ScalingView()
        {
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Draws the view
        /// </summary>
        /// <param name="drawing">drawing wrapper</param>
        /// <param name="options">drawing options</param>
        /// <param name="margin">drawing margin</param>
        public virtual void Draw(IDrawing drawing, IDrawOptions options, IMargin margin)
        {
            if (drawing == null || margin == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }

            if (!(options is ScalingDrawOptions))
            {
                throw new InvalidArgumentException("options is not compatible");
            }
            var state = ((ScalingDrawOptions)options).State;

            var canvasSize = options.CanvasSize;
            var clip = new RectangleF((float)margin.Left, (float)margin.Top, (float)(canvasSize.Width - margin.LeftAndRight), (float)(canvasSize.Height - margin.TopAndBottom));

            if (state.MovingStart != null && state.MovingPosition != null)
            {
                drawing.Line(state.MovingPenColor, state.MovingStart.Value.X, state.MovingStart.Value.Y, state.MovingPosition.Value.X, state.MovingPosition.Value.Y, clip);
            }

            if (state.ScalingStart != null && state.ScalingPosition != null)
            {
                Rectangle rectangle = SortCoordinates(state.ScalingStart.Value, state.ScalingPosition.Value);
                var color = state.ZoomIncrease ? state.ZoomInPenColor : state.ZoomOutPenColor;
                drawing.Rectangle(color, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, clip);
            }
        }
        #endregion

        #region Private methods
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
        #endregion
    }
}
