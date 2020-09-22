using System;

namespace GraphControlCore.Interfaces
{
    public interface IContainer : IDisposable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        void Register<TService, TImplementation>() where TImplementation : TService;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        void Register<TService>();
        void RegisterInstance<T>(T instance);
        TService Resolve<TService>();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        bool IsRegistered<TService>();
        TService GetInstance<TService>() where TService : class;
    }
}
