using GraphControl.Structs;

namespace GraphControl.Interfaces.Presenters
{
    public interface IDrawingPresenter
    {
        void Draw(IDrawing drawing, DrawOptions options, IMargin margin);
    }
}
