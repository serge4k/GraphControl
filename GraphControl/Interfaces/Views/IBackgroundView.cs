using GraphControlCore.Interfaces.Models;

namespace GraphControlCore.Interfaces.Views
{
    public interface IBackgroundView : IDrawingView
    {
        IBackgroundState State { get; set; }
    }
}