using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Presenters
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
