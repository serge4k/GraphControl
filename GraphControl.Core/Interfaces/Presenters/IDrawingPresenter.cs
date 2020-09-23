using GraphControl.Core.Structs;

namespace GraphControl.Core.Interfaces.Presenters
{
    public interface IDrawingPresenter
    {
        void Draw(IDrawing drawing, DrawOptions options, IMargin margin);
    }
}
