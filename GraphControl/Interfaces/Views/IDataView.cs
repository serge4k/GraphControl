using GraphControl.Interfaces.Models;

namespace GraphControl.Interfaces.Views
{
    public interface IDataView : IDrawingView
    {
        IGraphState State { get; set; }
    }
}