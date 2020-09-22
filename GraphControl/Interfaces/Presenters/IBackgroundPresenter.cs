using GraphControl.Interfaces.Views;

namespace GraphControl.Interfaces.Presenters
{
    public interface IBackgroundPresenter : IDrawingPresenter
    {
        IBackgroundView View { get;  set; }
    }
}
