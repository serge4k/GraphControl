using GraphControlCore.Interfaces;
using GraphControlCore.Interfaces.Presenters;
using GraphControlCore.Interfaces.Views;

namespace GraphControlCore.Presenters
{
    public abstract class BasePresenter<TView> : IPresenter 
        where TView : class, IView
    {
        public TView View { get; protected set; }
        protected IApplicationController Controller { get; private set; }

        protected BasePresenter(TView view) : this(null, view)
        {
        }

        protected BasePresenter(IApplicationController controller) : this(controller, null)
        {
        }

        protected BasePresenter(IApplicationController controller, TView view)
        {
            Controller = controller;
            View = view;
        }

        public virtual void Run()
        {

        }
    }
}
