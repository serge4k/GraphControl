using GraphControl.Interfaces;
using GraphControl.Interfaces.Presenters;
using GraphControl.Interfaces.Views;

namespace GraphControl.Utilites
{
    public class ApplicationController : IApplicationController
    {
        private readonly IContainer _container;

        public ApplicationController(IContainer container)
        {
            _container = container;
            _container.RegisterInstance<IApplicationController>(this);
        }

        public IApplicationController RegisterView<TView, TImplementation>()
            where TImplementation : class, TView
            where TView : IView
        {
            _container.Register<TView, TImplementation>();
            return this;
        }

        public IApplicationController RegisterViewInstance<TView, TImplementation>(TView instance, string viewName)
            where TImplementation : class, TView
            where TView : IView
        {
            _container.RegisterInstance<TView, TImplementation>(instance, viewName);
            return this;
        }

        public IApplicationController RegisterInstance<TInstance>(TInstance instance)
        {
            _container.RegisterInstance(instance);
            return this;
        }

        public IApplicationController RegisterService<TModel, TImplementation>()
            where TImplementation : class, TModel
        {
            _container.Register<TModel, TImplementation>();
            return this;
        }

        public TPresenter Run<TPresenter>() where TPresenter : class, IPresenter
        {
            if (!_container.IsRegistered<TPresenter>())
                _container.Register<TPresenter>();

            var presenter = _container.Resolve<TPresenter>();
            presenter.Run();
            return presenter;
        }

        public TPresenter Run<TPresenter, TArgument>(TArgument argument) where TPresenter : class, IPresenter<TArgument>
        {
            if (!_container.IsRegistered<TPresenter>())
                _container.Register<TPresenter>();

            var presenter = _container.Resolve<TPresenter>();
            presenter.Run(argument);
            return presenter;
        }


        public TPresenter Run<TPresenter, TArg0, TArg1>(TArg0 arg0, TArg1 arg1) where TPresenter : class, IPresenter<TArg0, TArg1>
        {
            if (!_container.IsRegistered<TPresenter>())
                _container.Register<TPresenter>();

            var presenter = _container.Resolve<TPresenter>();
            presenter.Run(arg0, arg1);
            return presenter;
        }

        public T GetInstance<T>()
            where T : class
        {
            return _container.GetInstance<T>();
        }

        public TView GetViewInstance<TView>(string viewName)
            where TView : class, IView
        {
            return _container.GetInstance<TView>(viewName);
        }
    }
}
