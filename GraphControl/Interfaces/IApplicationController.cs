using System;
using GraphControl.Interfaces.Presenters;
using GraphControl.Interfaces.Views;

namespace GraphControl.Interfaces
{
    public interface IApplicationController : IDisposable
    {
        IApplicationController RegisterInstance<TArgument>(TArgument instance);

        IApplicationController RegisterService<TService, TImplementation>()
            where TImplementation : class, TService;

        TPresenter Run<TPresenter>()
            where TPresenter : class, IPresenter;

        T GetInstance<T>()
            where T : class;
    }
}
