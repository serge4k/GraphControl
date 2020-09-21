using GraphControl.Interfaces;
using GraphControl.Interfaces.Presenters;
using GraphControl.Interfaces.Views;
using GraphControl.Structs;

namespace GraphControl.Presenters
{
    public class BackgroundPresenter : IBackgroundPresenter
    {
        private readonly IBackgroundView view;

        public BackgroundPresenter(IBackgroundView view)
        {
            this.view = view;
        }

        public void Draw(IDrawing drawing, DrawOptions drawOptions, IMargin margin)
        {
            this.view.Draw(drawing, drawOptions, margin);
        }
    }
}
