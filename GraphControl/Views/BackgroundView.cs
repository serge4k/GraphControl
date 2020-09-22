using GraphControl.Exceptions;
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

        public virtual void Draw(IDrawing drawing, DrawOptions options, IMargin margin)
        {
            if (drawing == null || margin == null)
            {
                throw new GraphControlException("parameter is null");
            }
            drawing.FillRectangle(this.State.PenColor, this.State.BackgroundColor, 0, 0, options.CanvasSize.Width - 1, options.CanvasSize.Height - 1);
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}
