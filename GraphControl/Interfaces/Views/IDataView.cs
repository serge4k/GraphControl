using GraphControlCore.Interfaces.Models;

namespace GraphControlCore.Interfaces.Views
{
    public interface IDataView : IDrawingView
    {
        IGraphState State { get; set; }
    }
}