using System;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Interfaces
{
    public interface IApplicationController : IDisposable
    {
        IApplicationController RegisterInstance<TArgument>(TArgument instance);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        IApplicationController RegisterService<TService, TImplementation>()
            where TImplementation : class, TService;

        TPresenter Run<TPresenter>()
            where TPresenter : class, IPresenter;

        T GetInstance<T>()
            where T : class;
    }
}
