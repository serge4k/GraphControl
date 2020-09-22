using System;

namespace GraphControl.Interfaces
{
    public interface IContainer : IDisposable
    {
        void Register<TService, TImplementation>() where TImplementation : TService;
        void Register<TService>();
        void RegisterInstance<T>(T instance);
        TService Resolve<TService>();
        bool IsRegistered<TService>();
        TService GetInstance<TService>() where TService : class;
    }
}
