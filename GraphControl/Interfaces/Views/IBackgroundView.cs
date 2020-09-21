using GraphControl.Interfaces.Models;

namespace GraphControl.Interfaces.Views
{
    public interface IBackgroundView : IDrawingView
    {
        IBackgroundState State { get; set; }
    }
}