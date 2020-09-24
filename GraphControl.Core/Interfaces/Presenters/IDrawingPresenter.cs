using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Interfaces.Presenters
{
    public interface IDrawingPresenter
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
