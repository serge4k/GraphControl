using GraphControl.Core.Interfaces.Models;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IDrawingView
    {
        /// <summary>
        /// Draws the view
        /// </summary>
        /// <param name="drawing">drawing wrapper</param>
        /// <param name="options">drawing options</param>
        /// <param name="margin">drawing margin</param>
        void Draw(IDrawing drawing, IDrawOptions options, IMargin margin);
    }
}
