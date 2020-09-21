using GraphControl.Interfaces.Presenters;
using GraphControl.Interfaces.Views;

namespace GraphControl.Interfaces
{
    public interface IApplicationController
    {
        IApplicationController RegisterView<TView, TImplementation>()
            where TImplementation : class, TView
            where TView : IView;

        IApplicationController RegisterViewInstance<TView, TImplementation>(TView instance, string viewName)
            where TImplementation : class, TView
            where TView : IView;

        IApplicationController RegisterInstance<TArgument>(TArgument instance);

        IApplicationController RegisterService<TService, TImplementation>()
            where TImplementation : class, TService;

        TPresenter Run<TPresenter>()
            where TPresenter : class, IPresenter;

        TPresenter Run<TPresenter, TArgument>(TArgument argument)
            where TPresenter : class, IPresenter<TArgument>;

        TPresenter Run<TPresenter, TArg0, TArg1>(TArg0 arg0, TArg1 arg1)
                    where TPresenter : class, IPresenter<TArg0, TArg1>;

        T GetInstance<T>()
            where T : class;

        TView GetViewInstance<TView>(string viewName)
            where TView : class, IView;
    }
}
