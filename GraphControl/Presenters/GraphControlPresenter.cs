using System;
using GraphControl.Events;
using GraphControl.Exceptions;
using GraphControl.Interfaces;
using GraphControl.Interfaces.Models;
using GraphControl.Interfaces.Presenters;
using GraphControl.Interfaces.Services;
using GraphControl.Interfaces.Views;
using GraphControl.Structs;

namespace GraphControl.Presenters
{
    public class GraphControlPresenter : BasePresenter<IGraphControlView>, IGraphControlPresenter
    {
        #region Public fields
        public event EventHandler<DataUpdatedEventArgs> DataUpdated
        {
            add
            {
                this.dataPresenter.DataUpdated += value;
            }
            remove
            {
                this.dataPresenter.DataUpdated -= value;
            }
        }
        #endregion

        #region Private fields
        private readonly IScaleService scaleService;
        private readonly IBufferedDrawingService bufferedDrawingService;
        private readonly IBackgroundPresenter backgroundPresenter;
        private readonly IGridPresenter gridPresenter;
        private readonly IDataPresenter dataPresenter;
        private readonly IScalingSelectionPresenter scalingSelectionPresenter;
        private IGraphControlFormState state;
        #endregion Private fields

        #region Constructors
        public GraphControlPresenter(IApplicationController controller, 
            IGraphControlView view,
            IScaleService scaleService,
            IBufferedDrawingService bufferedDrawingService,
            IBackgroundPresenter backgroundPresenter,
            IGridPresenter gridPresenter,
            IDataPresenter dataPresenter,
            IScalingSelectionPresenter scalingSelectionPresenter) : base(controller, view)
        {
            this.View = view;
            this.scaleService = scaleService;
            this.backgroundPresenter = backgroundPresenter;
            this.gridPresenter = gridPresenter;
            this.dataPresenter = dataPresenter;
            this.scalingSelectionPresenter = scalingSelectionPresenter;

            this.DataUpdated += GraphControlPresenter_DataUpdated;

            this.View.DrawGraphInBuffer += View_GraphPaintInBuffer;
            this.View.ControlSizeChanged += View_ControlSizeChanged;

            this.View.MouseDown += this.scalingSelectionPresenter.MouseDown;
            this.View.MouseMove += this.scalingSelectionPresenter.MouseMove;
            this.View.MouseUp += this.scalingSelectionPresenter.MouseUp;
            this.View.MouseWheel += this.scalingSelectionPresenter.MouseWheel;
            
            if (bufferedDrawingService == null)
            {
                throw new GraphControlException("parameter is null");
            }
            this.bufferedDrawingService = bufferedDrawingService;
            this.bufferedDrawingService.UpdateScale += BufferedDrawingService_UpdateScale;
            this.bufferedDrawingService.DrawGraph += BufferedDrawingService_DrawGraph;
            this.bufferedDrawingService.SetImage += BufferedDrawingService_SetImage;
        }

        #endregion

        #region Graph control inner presenter handlers
        private void GraphControlPresenter_DataUpdated(object sender, DataUpdatedEventArgs e)
        {
            bool force = this.scaleService.IsItemsVisible(e.Items);
            UpdateView(force);
        }

        public void OnLoad(LoadEventArgs e)
        {
            if (e == null)
            {
                throw new GraphControlException("parameter \"e\" is null");
            }
            this.View.SetBounds(e.Rect.Left, e.Rect.Top, e.Rect.Width, e.Rect.Height);
        }
        #endregion

        #region Buffer drawing service handlers
        private void View_GraphPaintInBuffer(object sender, DrawGraphEventArgs e)
        {
            // Update options from state
            var options = e.DrawOptions;
            this.bufferedDrawingService.DrawGraphInBufferAsync(options);
        }

        private void View_ControlSizeChanged(object sender, ControlSizeChangedEventArgs e)
        {
            ControlSizeChanged(e.CanvasSize);
        }

        private void BufferedDrawingService_UpdateScale(object sender, UpdateScaleEventArgs e)
        {
            UpdateScale(e.DrawOptions);
        }

        private void BufferedDrawingService_DrawGraph(object sender, DrawGraphEventArgs e)
        {
            var margin = this.scaleService.State.Margin;
            this.View.Draw(e.Drawing, e.DrawOptions, margin);
            this.backgroundPresenter.Draw(e.Drawing, e.DrawOptions, margin);
            this.gridPresenter.Draw(e.Drawing, e.DrawOptions, margin);
            this.dataPresenter.Draw(e.Drawing, e.DrawOptions, margin);
            this.scalingSelectionPresenter.Draw(e.Drawing, e.DrawOptions, margin);
        }

        private void BufferedDrawingService_SetImage(object sender, SetImageEventArgs e)
        {
            this.View.SetImage(e.Bitmap);
        }

        private void UpdateView(bool force)
        {
            var options = new DrawOptions(this.View.ControlSize, this.state.FitToScreenByX, this.state.FitToScreenByY);
            this.View.SetDrawOptions(options);

            if (force || this.state.FitToScreenAlways || this.state.FitToScreenByX || this.state.FitToScreenByY)
            {
                this.View.RefreshView();
            }
        }

        private void UpdateScale(Size canvasSize)
        {
            var options = new DrawOptions(canvasSize, this.state.FitToScreenByX, this.state.FitToScreenByY);
            UpdateScale(options);
        }
        #endregion

        #region Public methods
        public override void Run()
        {
        }

        public void UpdateFormState(IGraphControlFormState formState)
        {
            this.state = formState;
            UpdateView(false);
        }

        public void UpdateScale(DrawOptions options)
        {
            this.scaleService.UpdateScale(options);
        }

        public void ControlSizeChanged(Size canvasSize)
        {
            var size = new System.Drawing.Rectangle(0, 0, canvasSize.Width, canvasSize.Height);
            UpdateScale(canvasSize);
            UpdateView(true);
        }
        #endregion
    }
}
