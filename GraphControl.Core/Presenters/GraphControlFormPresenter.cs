using GraphControl.Core.Events;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Presenters;
using System;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Interfaces;

namespace GraphControl.Core.Presenters
{
    public class GraphControlFormPresenter : BasePresenter<IGraphControlFormView>, IGraphControlFormPresenter
    {
        public new IGraphControlFormView View { get; set; }

        #region Prevate fields
        private readonly IGraphControlFormState state;
        private readonly IGraphControlPresenter graphControlPresenter;
        #endregion

        #region Constructors
        public GraphControlFormPresenter(IApplicationController controller,
            IGraphControlFormView view,
            IGraphControlFormState state,
            IGraphControlPresenter graphControlPresenter
            ) : base(controller, view)
        {
            this.state = state;
            this.View = view;
            
            // Store inner presenters
            this.graphControlPresenter = graphControlPresenter;
            
            // Subsribe view events
            this.View.FitToScreenByX += View_FitToScreenByX;
            this.View.FitToScreenByY += View_FitToScreenByY;
            this.View.FitToScreenAlways += View_FitToScreenAlways;
            this.View.Load += View_Load;
            
            UpdateState();
        }
        #endregion

        #region Private methods
        private void UpdateState()
        {
            this.View.SetFitToScreenAlways(this.state.FitToScreenAlways);
            this.View.EnableFitByX(!this.state.FitToScreenAlways);
            this.View.EnableFitByY(!this.state.FitToScreenAlways);
            this.graphControlPresenter.UpdateFormState(this.state);
            if (!this.state.FitToScreenAlways)
            {
                this.state.FitToScreenByX = false;
                this.state.FitToScreenByY = false;
                this.graphControlPresenter.UpdateFormState(this.state);
            }
        }
        #endregion

        #region View event handlers
        private void View_FitToScreenByX(object sender, EventArgs e)
        {
            this.state.FitToScreenByX = true;
            this.state.FitToScreenAlways = false;
            UpdateState();
        }

        private void View_FitToScreenByY(object sender, EventArgs e)
        {
            this.state.FitToScreenByY = true;
            this.state.FitToScreenAlways = false;
            UpdateState();
        }

        private void View_FitToScreenAlways(object sender, FitToScreenAlwaysEventArgs e)
        {
            this.state.FitToScreenAlways = e.FitToScreenAlways;
            this.state.FitToScreenByX = true;
            this.state.FitToScreenByY = true;
            UpdateState();
        }

        private void View_Load(object sender, LoadEventArgs e)
        {
            this.graphControlPresenter.OnLoad(e);
        }
        #endregion

        #region Public methods
        public override void Run()
        {
            this.graphControlPresenter.Run();
            UpdateState();
        }
        #endregion
    }
}
