using GraphControl.Core.Interfaces.Models;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IDataView : IDrawingView
    {
        IGraphState State { get; set; }
    }
}