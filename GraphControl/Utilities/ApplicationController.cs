using System;
using GraphControl.Exceptions;
using GraphControl.Interfaces;
using GraphControl.Interfaces.Presenters;

namespace GraphControl.Utilities
{
    public class ApplicationController : IApplicationController
    {
        private readonly IContainer container;

        private bool disposed = false;

        public ApplicationController(IContainer container)
        {
            if (container == null)
            {
                throw new GraphControlException("parameter \"container\" is null");
            }

            this.container = container;
            this.container.RegisterInstance<IApplicationController>(this);
        }

        public IApplicationController RegisterInstance<TInstance>(TInstance instance)
        {
            container.RegisterInstance(instance);
            return this;
        }

        public IApplicationController RegisterService<TModel, TImplementation>()
            where TImplementation : class, TModel
        {
            container.Register<TModel, TImplementation>();
            return this;
        }

        public TPresenter Run<TPresenter>() where TPresenter : class, IPresenter
        {
            if (!container.IsRegistered<TPresenter>())
                container.Register<TPresenter>();

            var presenter = container.Resolve<TPresenter>();
            presenter.Run();
            return presenter;
        }

        public T GetInstance<T>()
            where T : class
        {
            return container.GetInstance<T>();
        }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free mamaged resources
                this.container.Dispose();
            }

            // Free unmanaged resources

            this.disposed = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
