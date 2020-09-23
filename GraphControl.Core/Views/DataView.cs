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
        public IGraphState State { get; set; }

        private readonly IScaleService scaleService;

        private readonly IDataService dataService;
                
        public DataView(IGraphState graphState, IScaleService scaleService, IDataService dataService)
        {
            if (graphState == null || scaleService == null || dataService == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            this.State = graphState;
            this.scaleService = scaleService;
            this.dataService = dataService;            
        }

        public virtual void Draw(IDrawing drawing, DrawOptions options, IMargin margin)
        {
            if (drawing == null || margin == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }

            var canvasSize = options.CanvasSize;
            var clip = new System.Drawing.RectangleF((float)margin.Left, (float)margin.Top, (float)(canvasSize.Width - margin.LeftAndRight), (float)(canvasSize.Height - margin.TopAndBottom));
            if (options.DrawOnlyNewData)
            {
                DrawNewData(drawing, options, margin, canvasSize, clip);
            }
            else
            {
                DrawAllData(drawing, margin, canvasSize, clip);
            }
        }

        private void DrawNewData(IDrawing drawing, DrawOptions options, IMargin margin, Size canvasSize, System.Drawing.RectangleF clip)
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
                        drawing.Circle(this.State.LineColor, margin.Left + x, canvasSize.Height - margin.Bottom - y, 4, clip);
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

                        drawing.Line(this.State.LineColor, margin.Left + x1, canvasSize.Height - margin.Bottom - y1, margin.Left + x2, canvasSize.Height - margin.Bottom - y2, clip);
                    }
                    prevItem = item;
                }
            }
        }

        private void DrawAllData(IDrawing drawing, IMargin margin, Size canvasSize, System.Drawing.RectangleF clip)
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
                        drawing.Circle(this.State.LineColor, margin.Left + x, canvasSize.Height - margin.Bottom - y, 4, clip);
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

                        drawing.Line(this.State.LineColor, margin.Left + x1, canvasSize.Height - margin.Bottom - y1, margin.Left + x2, canvasSize.Height - margin.Bottom - y2, clip);
                    }
                    
                    prevItem = item;
                }
            }
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}
