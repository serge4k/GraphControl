using GraphControl.Interfaces.Views;

namespace GraphControl.Interfaces.Presenters
{
    public interface IGraphControlFormPresenter : IPresenter
    {
        IGraphControlFormView View { get; set; }
    }
}
