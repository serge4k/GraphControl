using System;
using GraphControl.Definitions;
using GraphControl.Exceptions;
using GraphControl.Interfaces.Models;
using GraphControl.Interfaces.Services;
using GraphControl.Models;
using GraphControl.Interfaces;
using GraphControl.Structs;
using System.Collections.Generic;
using System.Linq;

namespace GraphControl.Services
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

        bool DataRangeEmpty
        {
            get
            {
                return this.State.X1 == this.State.X2 || this.State.X1 == this.State.X2;
            }
        }

        public event EventHandler StateStepUpdated;
        #endregion

        #region Private fields
        private IDataService dataService;
        private IScaleState scaleState;
        #endregion

        #region Constructors
        public ScaleService(IScaleState scaleState, IDataService dataService, IMargin margin)
        {
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
                throw new GraphControlException("ScaleX or ScaleY is zero error", ex);
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

        public void CanvasSizeChanged(DrawOptions drawOptions)
        {
            UpdateScale(drawOptions);
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
                throw new GraphControlException("ScaleX or ScaleY is zero error", ex);
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

        public void UpdateScale(DrawOptions options)
        {
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
            var margin = this.scaleState.Margin;

            double screenHeight = ScaleToScreenY(this.scaleState.Y2 - this.scaleState.Y1);

            double x = ToDataX(location.X - margin.Left);
            double y = ToDataY(screenHeight - location.Y); // Invert and to data

            double zoomTo = wheelDelta > 0 ? 1 : - 1D / 2;

            this.scaleState.X1 = this.scaleState.X1 - (x - this.scaleState.X1) * zoomTo;
            this.scaleState.Y1 = this.scaleState.Y1 - (y - this.scaleState.Y1) * zoomTo;
            this.scaleState.X2 = this.scaleState.X2 + (this.scaleState.X2 - x) * zoomTo;
            this.scaleState.Y2 = this.scaleState.Y2 + (this.scaleState.Y2 - y) * zoomTo;
        }

        public void Zoom(int wheelDelta)
        {
            double zoomTo = wheelDelta > 0 ? 1D / 2 : -1D / 4;

            double width = this.scaleState.X2 - this.scaleState.X1;
            double height = this.scaleState.Y2 - this.scaleState.Y1;

            this.scaleState.X1 = this.scaleState.X1 - width * zoomTo;
            this.scaleState.Y1 = this.scaleState.Y1 - height * zoomTo;
            this.scaleState.X2 = this.scaleState.X2 + width * zoomTo;
            this.scaleState.Y2 = this.scaleState.Y2 + height * zoomTo;
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
