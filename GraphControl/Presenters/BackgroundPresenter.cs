using GraphControlCore.Interfaces;
using GraphControlCore.Interfaces.Presenters;
using GraphControlCore.Interfaces.Views;
using GraphControlCore.Structs;

namespace GraphControlCore.Presenters
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
