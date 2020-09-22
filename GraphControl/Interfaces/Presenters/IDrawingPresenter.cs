using GraphControlCore.Structs;

namespace GraphControlCore.Interfaces.Presenters
{
    public interface IDrawingPresenter
    {
        void Draw(IDrawing drawing, DrawOptions options, IMargin margin);
    }
}
