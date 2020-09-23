using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Views
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
                throw new InvalidArgumentException("parameter is null");
            }
            drawing.FillRectangle(this.State.PenColor, this.State.BackgroundColor, 0, 0, options.CanvasSize.Width - 1, options.CanvasSize.Height - 1);
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}
