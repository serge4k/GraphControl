using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Interfaces.Presenters
{
    public interface IBackgroundPresenter : IDrawingPresenter
    {
        IBackgroundView View { get;  set; }
    }
}
