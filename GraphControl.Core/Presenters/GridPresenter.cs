using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Views;

namespace GraphControl.Core.Presenters
{
    public class GridPresenter : IGridPresenter, IDrawingPresenter
    {
        #region Public properties
        #endregion

        #region Private fields
        private readonly IGridView view;
        private readonly IGridState state;
        #endregion

        #region Contructors
        public GridPresenter(IGridView view, IGridState state)
        {
            this.view = view;
            this.state = state;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Draws the view
        /// </summary>
        /// <param name="drawing">drawing wrapper</param>
        /// <param name="options">drawing options</param>
        /// <param name="margin">drawing margin</param>
        public void Draw(IDrawing drawing, IDrawOptions options, IMargin margin)
        {
            this.view.Draw(drawing, new GridDrawOptions(options, state), margin);
        }
        #endregion
    }
}
