using System;
using LightInject;
using GraphControl.Interfaces;

namespace GraphControl.Utilities
{
    public sealed class DependInjectWrapper : IContainer, IDisposable
    {
        private readonly ServiceContainer container = new ServiceContainer();

        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            this.container.Register<TService, TImplementation>();
        }

        public void Register<TService>()
        {
            this.container.Register<TService>();
        }

        public void RegisterInstance<T>(T instance)
        {
            this.container.RegisterInstance(instance);
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

        public void Dispose()
        {
            this.container.Dispose();
        }
    }
}
