using GraphControl.Structs;

namespace GraphControl.Interfaces.Views
{
    public interface IDrawingView : IView
    {
        void Draw(IDrawing drawing, DrawOptions drawOptions, IMargin margin);
    }
}
