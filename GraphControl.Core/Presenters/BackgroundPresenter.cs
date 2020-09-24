using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Views;

namespace GraphControl.Core.Presenters
{
    public class BackgroundPresenter : IBackgroundPresenter
    {
        #region Public properties
        public IBackgroundView View { get; set; }
        #endregion

        #region Private fields
        private IBackgroundState state;
        #endregion

        #region Contructors
        public BackgroundPresenter(IBackgroundView view, IBackgroundState state)
        {
            this.View = view;
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
            this.View.Draw(drawing, new BackgroundDrawOptions(options, state), margin);
        }
        #endregion
    }
}
