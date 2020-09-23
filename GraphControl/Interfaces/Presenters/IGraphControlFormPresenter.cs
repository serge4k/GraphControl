using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Interfaces.Presenters
{
    public interface IGraphControlFormPresenter : IPresenter
    {
        IGraphControlFormView View { get; set; }
    }
}
