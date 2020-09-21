using System;
using System.Linq.Expressions;
using LightInject;
using GraphControl.Interfaces;

namespace GraphControl.Utilites
{
    public class DependInjectWrapper : IContainer
    {
        private readonly ServiceContainer container = new ServiceContainer();

        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            this.container.Register<TService, TImplementation>();
        }

        public void RegisterInstance<TService, TImplementation>(TService instance, string serviceName) where TImplementation : TService
        {
            this.container.RegisterInstance(instance, serviceName);
        }

        public void Register<TService>()
        {
            this.container.Register<TService>();
        }

        public void RegisterInstance<T>(T instance)
        {
            this.container.RegisterInstance(instance);
        }

        public void Register<TService, TArgument>(Expression<Func<TArgument, TService>> factory)
        {
            this.container.Register(serviceFactory => factory);
        }

        public TService Resolve<TService>()
        {
            return this.container.GetInstance<TService>();
        }

        public bool IsRegistered<TService>()
        {
            return this.container.CanGetInstance(typeof(TService), string.Empty);
        }

        public TService GetInstance<TService>() where TService : class
        {
            return this.container.GetInstance<TService>();
        }

        public TService GetInstance<TService>(string serviceName) where TService : class
        {
            return this.container.GetInstance(typeof(TService), serviceName) as TService;
        }
    }
}
