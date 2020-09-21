using GraphControl.Interfaces;
using GraphControl.Interfaces.Models;
using GraphControl.Interfaces.Views;
using GraphControl.Structs;

namespace GraphControl.Views
{
    public class BackgroundView : IBackgroundView
    {
        public IBackgroundState State { get; set; }

        public BackgroundView(IBackgroundState state)
        {
            this.State = state;
        }

        public void Draw(IDrawing drawing, DrawOptions drawOptions, IMargin margin)
        {
            drawing.FillRectangle(this.State.PenColor, this.State.BackgroundColor, 0, 0, drawOptions.CanvasSize.Width - 1, drawOptions.CanvasSize.Height - 1);
        }
    }
}
