using System;
using System.Linq.Expressions;

namespace GraphControl.Interfaces
{
    public interface IContainer
    {
        void Register<TService, TImplementation>() where TImplementation : TService;
        void RegisterInstance<TService, TImplementation>(TService instance, string serviceName) where TImplementation : TService;
        void Register<TService>();
        void RegisterInstance<T>(T instance);
        TService Resolve<TService>();
        bool IsRegistered<TService>();
        void Register<TService, TArgument>(Expression<Func<TArgument, TService>> factory);
        TService GetInstance<TService>() where TService : class;
        TService GetInstance<TService>(string serviceName) where TService : class;
    }
}
