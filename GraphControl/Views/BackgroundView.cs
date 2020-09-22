using GraphControlCore.Exceptions;
using GraphControlCore.Interfaces;
using GraphControlCore.Interfaces.Models;
using GraphControlCore.Interfaces.Views;
using GraphControlCore.Structs;

namespace GraphControlCore.Views
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
