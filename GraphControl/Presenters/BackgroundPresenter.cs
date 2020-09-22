using GraphControl.Interfaces;
using GraphControl.Interfaces.Presenters;
using GraphControl.Interfaces.Views;
using GraphControl.Structs;

namespace GraphControl.Presenters
{
    public class BackgroundPresenter : IBackgroundPresenter
    {
        public IBackgroundView View { get; set; }

        public BackgroundPresenter(IBackgroundView view)
        {
            this.View = view;
        }

        public void Draw(IDrawing drawing, DrawOptions options, IMargin margin)
        {
            this.View.Draw(drawing, options, margin);
        }
    }
}
