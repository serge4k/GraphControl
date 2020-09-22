using GraphControlCore.Interfaces.Views;

namespace GraphControlCore.Interfaces.Presenters
{
    public interface IBackgroundPresenter : IDrawingPresenter
    {
        IBackgroundView View { get;  set; }
    }
}
