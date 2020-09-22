using GraphControlCore.Structs;

namespace GraphControlCore.Interfaces.Views
{
    public interface IDrawingView : IView
    {
        void Draw(IDrawing drawing, DrawOptions options, IMargin margin);
    }
}
