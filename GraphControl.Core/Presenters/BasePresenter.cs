using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Presenters
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
