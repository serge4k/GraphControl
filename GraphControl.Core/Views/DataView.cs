using GraphControl.Core.Definitions;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Views
{
    public class DataView : IDataView
    {
        #region Private members
        private readonly IScaleService scaleService;

        /// <summary>
        ///  DataService access optimization
        /// </summary>
        private readonly IDataService dataService;
        #endregion

        #region Constructors
        public DataView(IScaleService scaleService, IDataService dataService)
        {
            if (scaleService == null || dataService == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            this.scaleService = scaleService;
            this.dataService = dataService;            
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Draws the view
        /// </summary>
        /// <param name="drawing">drawing wrapper</param>
        /// <param name="options">drawing options</param>
        /// <param name="margin">drawing margin</param>
        public virtual void Draw(IDrawing drawing, IDrawOptions options, IMargin margin)
        {
            if (drawing == null || margin == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }

            if (!(options is DataDrawOptions))
            {
                throw new InvalidArgumentException("options is not compatible");
            }
            var state = ((DataDrawOptions)options).State;

            var canvasSize = options.CanvasSize;
            var clip = new System.Drawing.RectangleF((float)margin.Left, (float)margin.Top, (float)(canvasSize.Width - margin.LeftAndRight), (float)(canvasSize.Height - margin.TopAndBottom));
            if (options.DrawOnlyNewData)
            {
                DrawNewData(state, drawing, options, margin, canvasSize, clip);
            }
            else
            {
                DrawAllData(state, drawing, margin, canvasSize, clip);
            }
        }
        #endregion

        #region Private methods
        private void DrawNewData(IDataDrawState state, IDrawing drawing, IDrawOptions options, IMargin margin, Size canvasSize, System.Drawing.RectangleF clip)
        {
            if (options.NewItems.Count == 1)
            {
                // Draw point for single data item
                foreach (var item in options.NewItems)
                {
                    if (this.scaleService.IsItemVisible(item))
                    {
                        var x = this.scaleService.ToScreen(Axis.X, item.X);
                        var y = this.scaleService.ToScreen(Axis.Y, item.Y);
                        drawing.Circle(state.LineColor, margin.Left + x, canvasSize.Height - margin.Bottom - y, 4, clip);
                    }
                }

            }
            else
            {
                IDataItem prevItem = null;
                // Draw lines
                foreach (var item in options.NewItems)
                {
                    if (item != null && prevItem != null
                        && (this.scaleService.IsItemVisible(prevItem)
                            || this.scaleService.IsItemVisible(item)))
                    {
                        var x1 = this.scaleService.ToScreen(Axis.X, prevItem.X);
                        var y1 = this.scaleService.ToScreen(Axis.Y, prevItem.Y);

                        var x2 = this.scaleService.ToScreen(Axis.X, item.X);
                        var y2 = this.scaleService.ToScreen(Axis.Y, item.Y);

                        drawing.Line(state.LineColor, margin.Left + x1, canvasSize.Height - margin.Bottom - y1, margin.Left + x2, canvasSize.Height - margin.Bottom - y2, clip);
                    }
                    prevItem = item;
                }
            }
        }

        private void DrawAllData(IDataDrawState state, IDrawing drawing, IMargin margin, Size canvasSize, System.Drawing.RectangleF clip)
        {
            var startX = this.scaleService.State.X1;
            var endX = this.scaleService.State.X2;

            if (this.dataService.ItemCount == 1)
            {
                // Draw point for single data item
                foreach (var item in this.dataService.GetItems(startX, endX))
                {
                    if (item != null && this.scaleService.IsItemVisible(item))
                    {
                        var x = this.scaleService.ToScreen(Axis.X, item.X);
                        var y = this.scaleService.ToScreen(Axis.Y, item.Y);
                        drawing.Circle(state.LineColor, margin.Left + x, canvasSize.Height - margin.Bottom - y, 4, clip);
                    }
                }

            }
            else
            {
                IDataItem prevItem = null;
                // Draw lines
                foreach (var item in this.dataService.GetItems(startX, endX))
                {
                    if (item != null && prevItem != null 
                        && (this.scaleService.IsItemVisible(prevItem)
                            || this.scaleService.IsItemVisible(item)))
                    {
                        var x1 = this.scaleService.ToScreen(Axis.X, prevItem.X);
                        var y1 = this.scaleService.ToScreen(Axis.Y, prevItem.Y);

                        var x2 = this.scaleService.ToScreen(Axis.X, item.X);
                        var y2 = this.scaleService.ToScreen(Axis.Y, item.Y);

                        drawing.Line(state.LineColor, margin.Left + x1, canvasSize.Height - margin.Bottom - y1, margin.Left + x2, canvasSize.Height - margin.Bottom - y2, clip);
                    }
                    
                    prevItem = item;
                }
            }
        }
        #endregion
    }
}
