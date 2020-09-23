using GraphControl.Core.Structs;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IDrawingView : IView
    {
        void Draw(IDrawing drawing, DrawOptions options, IMargin margin);
    }
}
