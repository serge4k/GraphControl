using GraphControlCore.Interfaces.Views;

namespace GraphControlCore.Interfaces.Presenters
{
    public interface IGraphControlFormPresenter : IPresenter
    {
        IGraphControlFormView View { get; set; }
    }
}
