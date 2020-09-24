using System;
using GraphControl.Core.Definitions;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Views;
using GraphControl.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Services
{
    public class ScaleService : IScaleService
    {
        #region Public properties
        public IScaleState State
        {
            get
            {
                return new ScaleState(this.scaleState); // Return copy of state
            }
        }

        public event EventHandler StateStepUpdated;

        private const double ZoomLimit = 10000;
        #endregion

        #region Private fields
        private IDataService dataService;
        private IScaleState scaleState;
        #endregion

        #region Constructors
        public ScaleService(IScaleState scaleState, IDataService dataService, IMargin margin)
        {
            if (scaleState == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            this.scaleState = scaleState;
            this.scaleState.Margin = margin;
            this.dataService = dataService;
        }
        #endregion

        #region Public methods
        public virtual double ToScreen(Axis axis, double value)
        {
            switch (axis)
            {
                case Axis.X:
                    return ToScreenX(value);
                case Axis.Y:
                    return ToScreenY(value);
                default:
                    throw new GraphControlException($"Axis index {axis.ToString()} is not supported");
            }
        }

        public double ToScreenX(double value)
        {
            return ScaleToScreenX(value - this.scaleState.X1);
        }

        public double ToScreenY(double value)
        {
            return ScaleToScreenY(value - this.scaleState.Y1);
        }
        
        public virtual double ScaleToScreen(Axis axis, double value)
        {
            switch (axis)
            {
                case Axis.X:
                    return ScaleToScreenX(value);
                case Axis.Y:
                    return ScaleToScreenY(value);
                default:
                    throw new GraphControlException($"Axis index {axis.ToString()} is not supported");
            }
        }

        public double ScaleToScreenX(double value)
        {
            return value * this.scaleState.ScaleX;
        }

        public double ScaleToScreenY(double value)
        {
            return value * this.scaleState.ScaleY;
        }

        public virtual double ToData(Axis axis, double value)
        {
            try
            {
                switch (axis)
                {
                    case Axis.X:
                        return ToDataX(value);
                    case Axis.Y:
                        return ToDataY(value);
                    default:
                        throw new GraphControlException($"Axis index {axis.ToString()} is not supported");
                }
            }            
            catch (DivideByZeroException ex)
            {
                throw new GraphControlException("Scales zero error", ex);
            }
        }

        public double ToDataX(double value)
        {
            return ScaleToDataX(value) + this.scaleState.X1;
        }

        public double ToDataY(double value)
        {
            return ScaleToDataY(value) + this.scaleState.Y1;
        }

        public double ScaleToData(Axis axis, double value)
        {
            try
            {
                switch (axis)
                {
                    case Axis.X:
                        return ScaleToDataX(value);
                    case Axis.Y:
                        return ScaleToDataY(value);
                    default:
                        throw new GraphControlException($"Axis index {axis.ToString()} is not supported");
                }
            }
            catch (DivideByZeroException ex)
            {
                throw new GraphControlException("Divide by zero error", ex);
            }
        }

        public double ScaleToDataX(double value)
        {
            return value / this.scaleState.ScaleX;
        }

        public double ScaleToDataY(double value)
        {
            return value / this.scaleState.ScaleY;
        }

        public void SetStep(Axis axis, double value)
        {
            try
            {
                switch (axis)
                {
                    case Axis.X:
                        SetStepX(value);
                        break;
                    case Axis.Y:
                        SetStepY(value);
                        break;
                    default:
                        throw new GraphControlException($"Axis index {axis.ToString()} is not supported");
                }
            }
            catch (DivideByZeroException ex)
            {
                throw new GraphControlException("Divide by zero error", ex);
            }
        }

        public void SetStepX(double value)
        {
            this.scaleState.StepX = value;
            this.StateStepUpdated?.Invoke(this, new EventArgs());
        }

        public void SetStepY(double value)
        {
            this.scaleState.StepY = value;
            this.StateStepUpdated?.Invoke(this, new EventArgs());
        }

        public void UpdateScale(IDrawOptions options)
        {
            if (options == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }

            var canvasSize = options.CanvasSize;
            var margin = this.scaleState.Margin;
            if (options.FitToX)
            {
                this.scaleState.X1 = this.dataService.GetMin(Axis.X);
                this.scaleState.X2 = this.dataService.GetMax(Axis.X);
            }

            var x1 = this.scaleState.X1;
            var x2 = this.scaleState.X2;
            if (x1 == 0 && x2 == 0)
            {
                this.scaleState.ScaleX = 1;
            }
            else if (x1 == x2)
            {
                this.scaleState.ScaleX = 1;
            }
            else
            {
                if (canvasSize.Height < margin.Top - margin.Bottom)
                {
                    throw new GraphControlException($"Control size is too small (Width: {canvasSize.Width} less than margin: {margin.Left - margin.Right} ");
                }
                this.scaleState.ScaleX = (canvasSize.Width - margin.Left - margin.Right) / (x2 - x1);
            }


            if (options.FitToY)
            {
                this.scaleState.Y1 = this.dataService.GetMin(Axis.Y);
                this.scaleState.Y2 = this.dataService.GetMax(Axis.Y);
            }

            var y1 = this.scaleState.Y1;
            var y2 = this.scaleState.Y2;

            if (y1 == 0 && y2 == 0)
            {
                this.scaleState.ScaleY = 1;
            }
            else if (y1 == y2)
            {
                this.scaleState.ScaleY = 1;
            }
            else
            {
                if (canvasSize.Height < margin.Top - margin.Bottom)
                {
                    throw new GraphControlException($"Control size is too small (Height: {canvasSize.Height} less than margin: {margin.Top - margin.Bottom} ");
                }
                this.scaleState.ScaleY = (canvasSize.Height - margin.Top - margin.Bottom) / (y2 - y1);
            }
        }

        public void UpdateMargin(IMargin margin)
        {
            this.scaleState.Margin = margin;
        }

        public void Zoom(System.Drawing.Rectangle rectangle, bool increase)
        {
            var margin = this.scaleState.Margin;

            double deltaY = this.scaleState.Y2 - this.scaleState.Y1;

            double screenHeight = ScaleToScreenY(deltaY);

            double x1 = ToDataX(rectangle.Left - margin.Left);
            double y1 = ToDataY(screenHeight - rectangle.Bottom); // Invert and to data
            double x2 = ToDataX(rectangle.Right - margin.Left);
            double y2 = ToDataY(screenHeight - rectangle.Top); // Invert and to data

            if (increase)
            {
                this.scaleState.X1 = x1;
                this.scaleState.Y1 = y1;
                this.scaleState.X2 = x2;
                this.scaleState.Y2 = y2;
            }
            else
            {
                this.scaleState.X1 -= Math.Abs(x1 - this.scaleState.X1);
                this.scaleState.Y1 -= Math.Abs(y1 - this.scaleState.Y1);
                this.scaleState.X2 += Math.Abs(this.scaleState.X2 - x2);
                this.scaleState.Y2 += Math.Abs(this.scaleState.Y2 - y2);
            }            
        }

        public void Zoom(System.Drawing.Point location, int wheelDelta)
        {
            if (wheelDelta == 0)
            {
                return;
            }
            var margin = this.scaleState.Margin;

            double screenHeight = ScaleToScreenY(this.scaleState.Y2 - this.scaleState.Y1);

            double x = ToDataX(location.X - margin.Left);
            double y = ToDataY(screenHeight - location.Y); // Invert and to data

            double zoomTo = wheelDelta > 0 ? 1 : - 1D / 2;

            double newX1 = this.scaleState.X1 - (x - this.scaleState.X1) * zoomTo;
            double newX2 = this.scaleState.X2 + (this.scaleState.X2 - x) * zoomTo;
            double diffX = this.dataService.GetMax(Axis.X) - this.dataService.GetMin(Axis.X);

            double newY1 = this.scaleState.Y1 - (y - this.scaleState.Y1) * zoomTo;
            double newY2 = this.scaleState.Y2 + (this.scaleState.Y2 - y) * zoomTo;
            double diffY = this.dataService.GetMax(Axis.Y) - this.dataService.GetMin(Axis.Y);

            // Limit zooming diff to 1/10000 and 10000x
            if (newX2 - newX1 > diffX / ScaleService.ZoomLimit && newX2 - newX1 < diffX * ScaleService.ZoomLimit
                && newY2 - newY1 > diffY / ScaleService.ZoomLimit && newY2 - newY1 < diffY * ScaleService.ZoomLimit)
            {
                this.scaleState.X1 = newX1;
                this.scaleState.X2 = newX2;
                this.scaleState.Y1 = newY1;
                this.scaleState.Y2 = newY2;
            }
        }

        public void Zoom(int wheelDelta)
        {
            if (wheelDelta == 0)
            {
                return;
            }
            checked
            {
                double zoomTo = wheelDelta > 0 ? 1D / 2 : -1D / 4;

                double width = this.scaleState.X2 - this.scaleState.X1;
                double newX1 = this.scaleState.X1 - width * zoomTo;
                double newX2 = this.scaleState.X2 + width * zoomTo;
                double diffX = this.dataService.GetMax(Axis.X) - this.dataService.GetMin(Axis.X);

                double height = this.scaleState.Y2 - this.scaleState.Y1;
                double newY1 = this.scaleState.Y1 - height * zoomTo;
                double newY2 = this.scaleState.Y2 + height * zoomTo;
                double diffY = this.dataService.GetMax(Axis.Y) - this.dataService.GetMin(Axis.Y);

                // Limit zooming diff to 1/10000 and 10000x
                if (newX2 - newX1 > diffX / ScaleService.ZoomLimit && newX2 - newX1 < diffX * ScaleService.ZoomLimit
                    && newY2 - newY1 > diffY / ScaleService.ZoomLimit && newY2 - newY1 < diffY * ScaleService.ZoomLimit)
                {
                    this.scaleState.X1 = newX1;
                    this.scaleState.X2 = newX2;
                    this.scaleState.Y1 = newY1;
                    this.scaleState.Y2 = newY2;
                }
            }
        }

        public void Move(int offsetX, int offsetY)
        {
            double xDataOffset = ScaleToDataX(offsetX);
            double yDataOffset = ScaleToDataY(offsetY);
            this.scaleState.X1 += xDataOffset;
            this.scaleState.Y1 -= yDataOffset;
            this.scaleState.X2 += xDataOffset;
            this.scaleState.Y2 -= yDataOffset;
        }

        public bool IsItemVisible(IDataItem item)
        {
            if (item == null)
            {
                throw new InvalidArgumentException("parameter \"item\" is null");
            }

            return item.X >= this.State.X1 && item.X <= this.State.X2 &&
                item.Y >= this.State.Y1 && item.Y <= this.State.Y2;
        }

        public bool IsItemsVisible(ICollection<IDataItem> items)
        {
            return items.Where((item) => IsItemVisible(item)).Count() > 0;
        }
        #endregion

        #region Private methods
        #endregion
    }
}
