using System.Drawing;
using GraphControl.Core.Definitions;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Presenters;
using GraphControl.Core.Structs;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Views;

namespace GraphControl.Core.Presenters
{
    public class ScalingSelectionPresenter : IScalingSelectionPresenter
    {
        #region Private fields
        private readonly IScalingSelectionView view;
        private readonly IScalingState state;
        private readonly IRefreshControlView rootControlView;
        private readonly IScaleService scaleService;
        #endregion

        #region Contructors
        public ScalingSelectionPresenter(IScalingSelectionView view, IScalingState state, IRefreshControlView rootControlView, IScaleService scaleService) 
            : this(view, state, rootControlView, scaleService, Color.FromArgb(200, Color.DarkViolet), Color.FromArgb(223, 63, 63, 191), Color.FromArgb(223, 63, 191, 63))
        {
        }

        public ScalingSelectionPresenter(IScalingSelectionView view, IScalingState state, IRefreshControlView rootControlView, IScaleService scaleService, Color movingPenColor, Color zoomInPenColor, Color zoomOutPenColor)
        {
            if (view == null || state == null )
            {
                throw new InvalidArgumentException("parameter \"view\" or \"state\" is null");
            }
            this.view = view;
            this.state = state;
            this.rootControlView = rootControlView;
            this.scaleService = scaleService;
            this.state.MovingPenColor = movingPenColor;
            this.state.ZoomInPenColor = zoomInPenColor;
            this.state.ZoomOutPenColor = zoomOutPenColor;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Draws the view
        /// </summary>
        /// <param name="drawing">drawing wrapper</param>
        /// <param name="options">drawing options</param>
        /// <param name="margin">drawing margin</param>
        public void Draw(IDrawing drawing, IDrawOptions options, IMargin margin)
        {
            this.view.Draw(drawing, new ScalingDrawOptions(options, this.state), margin);
        }

        public void MouseDown(object sender, ScaleUserSelectionEventArgs e)
        {
            if (e == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            switch (e.Button)
            {
                case MouseButton.Left:
                    this.state.ZoomIncrease = !e.ShiftPressed;
                    this.state.ScalingStart = e.Location;
                    break;
                case MouseButton.Right:
                    this.state.MovingStart = e.Location;
                    break;
            }
        }

        public void MouseMove(object sender, ScaleUserSelectionEventArgs e)
        {
            if (e == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            switch (e.Button)
            {
                case MouseButton.Left:
                    if (this.state.ScalingStart != null)
                    {
                        this.state.ScalingPosition = e.Location;
                        this.rootControlView.RefreshView();
                    }
                    break;
                case MouseButton.Right:
                    if (this.state.MovingStart != null)
                    {
                        if (this.state.MovingPosition != null)
                        {
                            Shift(e.Location.X - this.state.MovingPosition.Value.X, e.Location.Y - this.state.MovingPosition.Value.Y);
                        }                        
                        this.state.MovingPosition = e.Location;
                        this.rootControlView.RefreshView();
                    }
                    break;
            }
        }

        public void MouseUp(object sender, ScaleUserSelectionEventArgs e)
        {
            if (e == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            switch (e.Button)
            {
                case MouseButton.Left:
                    if (this.state.ScalingStart != null && this.state.ScalingPosition != null)
                    {
                        Scale(this.state.ScalingStart.Value, e.Location, this.state.ZoomIncrease);
                        this.state.ScalingStart = null;
                        this.state.ScalingPosition = null;
                        this.rootControlView.RefreshView();
                    }
                    break;
                case MouseButton.Right:
                    if (this.state.MovingStart != null && this.state.MovingPosition != null)
                    {
                        this.state.MovingStart = null;
                        this.state.MovingPosition = null;
                        this.rootControlView.RefreshView();
                    }
                    break; 
            }
        }

        public void MouseWheel(object sender, ScaleUserSelectionEventArgs e)
        {
            if (e == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            if (e.ShiftPressed)
            {
                Scale(e.Location, e.WheelDelta);
            }
            else
            {
                Scale(e.WheelDelta);
            }
            
            this.rootControlView.RefreshView();
        }
        #endregion

        #region Private methods
        private void Scale(Point start, Point end, bool increase)
        {
            Rectangle rectangle = SortCoordinates(start, end);
            this.scaleService.Zoom(rectangle, increase);
        }

        private static Rectangle SortCoordinates(Point scalingStart, Point scalingPos)
        {
            var x1 = scalingStart.X;
            var x2 = scalingPos.X;
            var y1 = scalingStart.Y;
            var y2 = scalingPos.Y;
            if (x1 > x2)
            {
                x2 = scalingStart.X;
                x1 = scalingPos.X;
            }
            if (y1 > y2)
            {
                y2 = scalingStart.Y;
                y1 = scalingPos.Y;
            }
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        private void Scale(int wheelDelta)
        {
            this.scaleService.Zoom(wheelDelta);
        }

        private void Scale(Point location, int wheelDelta)
        {
            this.scaleService.Zoom(location, wheelDelta);
        }

        private void Shift(int x, int y)
        {
            this.scaleService.Move(-x, -y);
        }
        #endregion
    }
}
