using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Models;

namespace GraphControl.Core.Views
{
    public class BackgroundView : IBackgroundView
    {
        #region Constructors
        public BackgroundView()
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
            if (!(options is BackgroundDrawOptions))
            {
                throw new InvalidArgumentException("options is not compatible");
            }
            var state = ((BackgroundDrawOptions)options).State;
            drawing.FillRectangle(state.PenColor, state.BackgroundColor, 0, 0, options.CanvasSize.Width - 1, options.CanvasSize.Height - 1);
        }
        #endregion
    }
}
