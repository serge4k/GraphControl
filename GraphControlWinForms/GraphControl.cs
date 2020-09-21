using System;
using System.Windows.Forms;
using GraphControl.Definitions;
using GraphControl.Events;
using GraphControl.Factory;
using GraphControl.Interfaces;
using GraphControl.Interfaces.Presenters;
using GraphControl.Interfaces.Services;
using GraphControl.Interfaces.Views;
using GraphControl.Structs;
using GraphControl.Utilites;

namespace GraphControlWinForms
{
    public partial class GraphControl : UserControl, IGraphControlFormView
    {
        #region Public properties
        public event EventHandler FitToScreenByX;

        public event EventHandler FitToScreenByY;

        public event EventHandler<FitToScreenAlwaysEventArgs> FitToScreenAlways;

        public new event EventHandler<LoadEventArgs> Load;

        public event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;

        public Size ControlSize => new Size(this.ClientSize.Width, this.ClientSize.Height);

        public IItemFormatter ItemFormatter { get; private set; }

        public Padding GraphPadding
        {
            get
            {
                return graphPadding;
            }

            set
            {
                graphPadding = value;
                this.scaleService.UpdateMargin(this.GraphMargin);
            }
        }

        public Margin GraphMargin
        {
            get
            {
                return new Margin(this.GraphPadding.Left, this.GraphPadding.Top, this.GraphPadding.Right, this.GraphPadding.Bottom);
            }
        }
        #endregion

        #region Private fields
        private Padding graphPadding;
        private IApplicationController applicationController;
        
        // To optimize interations we will use direct links to anoter services and presenters directly
        private IDataService dataService;
        private IScaleService scaleService;
        private IGridPresenter gridPresenter;       
        #endregion

        #region Constructors
        public GraphControl()
        {
            InitializeComponent();

            CreatePreseter();

            CreateControls();

            base.Load += GraphControl_Load;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// To make the user control autonomous create inner GraphControl Presenter manually instead of dependency injection
        /// and store it to the Form field (this method violates MVP design rules)
        /// </summary>
        private void CreatePreseter()
        {
            // Create ApplicationController
            this.applicationController = GraphControlFactory.Factory.CreateController();

            this.ItemFormatter = new ItemFormatter();
            this.ItemFormatter.Register(Axis.X, new DoubleValueFormatter());
            this.ItemFormatter.Register(Axis.Y, new DoubleValueFormatter());

            this.graphPadding = new Padding(80, 5, 5, 40);

            // Register dependencies
            GraphControlFactory.Factory.RegisterInstances(this.applicationController, this, new GraphControlView(), this.ItemFormatter, this.GraphMargin, true);

            // To optimize interations we will use direct links to anoter services and presenters directly
            this.dataService = this.applicationController.GetInstance<IDataService>();
            this.scaleService = this.applicationController.GetInstance<IScaleService>();
            this.scaleService.StateStepUpdated += ScaleService_StateStepUpdated;
            this.gridPresenter = this.applicationController.GetInstance<IGridPresenter>();            
        }

        private void CreateControls()
        {
            var graphControlView = (GraphControlView)this.applicationController.GetInstance<IGraphControlView>();
                
            this.Controls.Add(graphControlView);
        }

        private void GraphControl_Load(object sender, EventArgs e)
        {
            var size = new Size(this.ControlSize.Width, this.ControlSize.Height);
            size.Height = this.btnFitToScreenByTime.Top - 10;
            var args = new LoadEventArgs(new Rect(0, 0, size.Width, size.Height));
            this.Load?.Invoke(this, args);
        }

        private void ScaleService_StateStepUpdated(object sender, EventArgs e)
        {
            this.SetDivisionValues(this.scaleService.State.StepX, this.scaleService.State.StepY);
        }
        #endregion

        #region Interfaces implementaion
        public void SetFitToScreenAlways(bool isChecked)
        {
            this.cbFitToScreenAlways.Checked = isChecked;
        }

        public void SetDivisionValues(double divX, double divY)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetDivisionValues(divX, divY)));
            }
            else
            {
                this.lblDivX.Text = this.ItemFormatter.ToStepString(Axis.X, divX);
                this.lblDivY.Text = this.ItemFormatter.ToStepString(Axis.Y, divY);
            }            
        }

        public void RegisterDataProvider(IGraphDataProvider dataProvider)
        {
            this.dataService.RegisterDataProvider(dataProvider);
        }
        #endregion

        #region Form event handlers
        private void btnFitToScreenByTime_Click(object sender, EventArgs e)
        {
            this.FitToScreenByX?.Invoke(this, new EventArgs());
        }

        private void btnFitToSreenByValue_Click(object sender, EventArgs e)
        {
            this.FitToScreenByY?.Invoke(this, new EventArgs());
        }

        private void cbFitToScreenAlways_CheckedChanged(object sender, EventArgs e)
        {
            this.FitToScreenAlways?.Invoke(this, new FitToScreenAlwaysEventArgs(cbFitToScreenAlways.Checked));
        }

        public void EnableFitByX(bool enabled)
        {
            btnFitToScreenByTime.Enabled = enabled;
        }

        public void EnableFitByY(bool enabled)
        {
            btnFitToSreenByValue.Enabled = enabled;
        }
        #endregion
    }
}
