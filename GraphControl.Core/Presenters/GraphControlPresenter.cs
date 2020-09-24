using System;
using System.Collections.Generic;
using GraphControl.Core.Events;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;
using GraphControl.Core.Views;

namespace GraphControl.Core.Presenters
{
    public class GraphControlPresenter : BasePresenter<IGraphControlView>, IGraphControlPresenter
    {
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
            IDataService dataService,
            IScaleService scaleService,
            IBufferedDrawingService bufferedDrawingService,
            IBackgroundPresenter backgroundPresenter,
            IGridPresenter gridPresenter,
            IDataPresenter dataPresenter,
            IScalingSelectionPresenter scalingSelectionPresenter) : base(controller, view)
        {
            if (dataService == null)
            {
                throw new InvalidArgumentException("data sevice is null");
            }
            this.scaleService = scaleService;
            this.backgroundPresenter = backgroundPresenter;
            this.gridPresenter = gridPresenter;
            this.dataPresenter = dataPresenter;
            this.scalingSelectionPresenter = scalingSelectionPresenter;

            dataService.DataUpdated += GraphControlPresenter_DataUpdated;

            this.View.DrawGraph += View_DrawGraph;
            this.View.ControlSizeChanged += View_ControlSizeChanged;

            this.View.MouseDown += this.scalingSelectionPresenter.MouseDown;
            this.View.MouseMove += this.scalingSelectionPresenter.MouseMove;
            this.View.MouseUp += this.scalingSelectionPresenter.MouseUp;
            this.View.MouseWheel += this.scalingSelectionPresenter.MouseWheel;
            
            if (bufferedDrawingService == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            this.bufferedDrawingService = bufferedDrawingService;
            this.bufferedDrawingService.UpdateScale += BufferedDrawingService_UpdateScale;
            this.bufferedDrawingService.DrawGraph += BufferedDrawingService_DrawGraph;
            this.bufferedDrawingService.SetImage += BufferedDrawingService_SetImage;
        }

        #endregion

        #region Graph control inner presenter handlers
        /// <summary>
        /// Data updated handler of the DataService event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphControlPresenter_DataUpdated(object sender, DataUpdatedEventArgs e)
        {
            bool force = this.scaleService.IsItemsVisible(e.Items);
            UpdateView(force, e.Items);
        }

        /// <summary>
        /// Updates control size after load of the parent control
        /// </summary>
        /// <param name="e"></param>
        public void OnLoad(LoadEventArgs e)
        {
            if (e == null)
            {
                throw new InvalidArgumentException("parameter \"e\" is null");
            }
            this.View.SetBounds(e.Rect.Left, e.Rect.Top, e.Rect.Width, e.Rect.Height);
        }
        #endregion

        #region Buffer drawing service handlers
        private void View_DrawGraph(object sender, DrawGraphEventArgs e)
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
            this.scaleService.UpdateScale(e.DrawOptions);
        }

        private void BufferedDrawingService_DrawGraph(object sender, DrawGraphEventArgs e)
        {
            var margin = this.scaleService.State.Margin;
            if (!e.DrawOptions.DrawOnlyNewData)
            {
                // When no new items, then if whould be request to update all like resize
                this.backgroundPresenter.Draw(e.Drawing, e.DrawOptions, margin);
                this.gridPresenter.Draw(e.Drawing, e.DrawOptions, margin);
            }
            this.dataPresenter.Draw(e.Drawing, e.DrawOptions, margin);
            this.scalingSelectionPresenter.Draw(e.Drawing, e.DrawOptions, margin);
        }

        private void BufferedDrawingService_SetImage(object sender, SetImageEventArgs e)
        {
            this.View.SetImage(e.Bitmap);
        }

        private void UpdateView(bool force, ICollection<IDataItem> items)
        {
            var options = new DrawOptions(this.View.ControlSize, this.state.FitToScreenByX, this.state.FitToScreenByY, items);
            
            if (force || this.state.FitToScreenAlways || this.state.FitToScreenByX || this.state.FitToScreenByY)
            {
                this.View.RefreshView(options);
            }
        }

        private void UpdateScale(Size canvasSize, ICollection<IDataItem> newItems)
        {
            var options = new DrawOptions(canvasSize, this.state.FitToScreenByX, this.state.FitToScreenByY, newItems);
            this.scaleService.UpdateScale(options);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// No special action
        /// </summary>
        public override void Run()
        {
        }

        /// <summary>
        /// Called from parent presenter (GraphControlFormPresenter) to nofity about FitByX or FitByY was changed
        /// </summary>
        /// <param name="formState">updated state</param>
        public void UpdateFormState(IGraphControlFormState formState)
        {
            this.state = formState;
            UpdateView(false, null); // Update view FixByX or Y state was changed
        }

        /// <summary>
        /// Called from parent presenter (GraphControlFormPresenter) to nofity about control size was changed
        /// </summary>
        /// <param name="canvasSize">new size</param>
        public void ControlSizeChanged(Size canvasSize)
        {
            UpdateScale(canvasSize, null);
            UpdateView(true, null);
        }
        #endregion
    }
}
