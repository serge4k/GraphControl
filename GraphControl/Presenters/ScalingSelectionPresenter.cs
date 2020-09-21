using System.Drawing;
using GraphControl.Definitions;
using GraphControl.Events;
using GraphControl.Interfaces.Presenters;
using GraphControl.Interfaces.Services;
using GraphControl.Interfaces.Views;
using GraphControl.Interfaces;
using GraphControl.Presenters;
using GraphControl.Structs;

namespace GraphControl.Presenters
{
    public class ScalingSelectionPresenter : IScalingSelectionPresenter, IDrawingPresenter
    {
        #region Public properties
        #endregion

        #region Private fields
        private readonly IScalingSelectionView view;
        private readonly IControlView rootControlView;
        private readonly IScaleService scaleService;
        #endregion

        #region Contructors
        public ScalingSelectionPresenter(IScalingSelectionView view, IControlView rootControlView, IScaleService scaleService) 
            : this(view, rootControlView, scaleService, Color.FromArgb(200, Color.DarkViolet), Color.FromArgb(223, 63, 63, 191), Color.FromArgb(223, 63, 191, 63))
        {
        }

        public ScalingSelectionPresenter(IScalingSelectionView view, IControlView rootControlView, IScaleService scaleService, Color movingPenColor, Color zoomInPenColor, Color zoomOutPenColor) 
        {
            this.view = view;
            this.rootControlView = rootControlView;
            this.scaleService = scaleService;
            this.view.MovingPenColor = movingPenColor;
            this.view.ZoomInPenColor = zoomInPenColor;
            this.view.ZoomOutPenColor = zoomOutPenColor;
        }
        #endregion

        #region Public methods
        public void Draw(IDrawing drawing, DrawOptions drawOptions, IMargin margin)
        {
            this.view.Draw(drawing, drawOptions, margin);
        }

        public void MouseDown(object sender, ScaleUserSelectionEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.view.ZoomIncrease = !e.ShiftPressed;
                    this.view.ScalingStart = e.Location;
                    break;
                case MouseButtons.Right:
                    this.view.MovingStart = e.Location;
                    break;
            }
        }

        public void MouseMove(object sender, ScaleUserSelectionEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (this.view.ScalingStart != null)
                    {
                        this.view.ScalingPos = e.Location;
                        this.rootControlView.RefreshView();
                    }
                    break;
                case MouseButtons.Right:
                    if (this.view.MovingStart != null)
                    {
                        if (this.view.MovingPos != null)
                        {
                            Shift(e.Location.X - this.view.MovingPos.Value.X, e.Location.Y - this.view.MovingPos.Value.Y);
                        }                        
                        this.view.MovingPos = e.Location;
                        this.rootControlView.RefreshView();
                    }
                    break;
            }
        }

        public void MouseUp(object sender, ScaleUserSelectionEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (this.view.ScalingStart != null && this.view.ScalingPos != null)
                    {
                        Scale(this.view.ScalingStart.Value, e.Location, this.view.ZoomIncrease);
                        this.view.ScalingStart = null;
                        this.view.ScalingPos = null;
                        this.rootControlView.RefreshView();
                    }
                    break;
                case MouseButtons.Right:
                    if (this.view.MovingStart != null && this.view.MovingPos != null)
                    {
                        this.view.MovingStart = null;
                        this.view.MovingPos = null;
                        this.rootControlView.RefreshView();
                    }
                    break; 
            }
        }

        public void MouseWheel(object sender, ScaleUserSelectionEventArgs e)
        {
            Scale(e.WheelDelta);
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
