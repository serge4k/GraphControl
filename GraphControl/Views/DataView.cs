using GraphControlCore.Definitions;
using GraphControlCore.Exceptions;
using GraphControlCore.Interfaces;
using GraphControlCore.Interfaces.Models;
using GraphControlCore.Interfaces.Services;
using GraphControlCore.Interfaces.Views;
using GraphControlCore.Structs;

namespace GraphControlCore.Views
{
    public class DataView : IDataView
    {
        public IGraphState State { get; set; }

        private readonly IScaleService scaleService;

        private readonly IDataService dataService;
                
        public DataView(IGraphState graphState, IScaleService scaleService, IDataService dataService)
        {
            this.State = graphState;
            this.scaleService = scaleService;
            this.dataService = dataService;            
        }

        public virtual void Draw(IDrawing drawing, DrawOptions options, IMargin margin)
        {
            if (drawing == null || margin == null)
            {
                throw new GraphControlException("parameter is null");
            }
            var canvasSize = options.CanvasSize;
            var clip = new System.Drawing.RectangleF((float)margin.Left, (float)margin.Top, (float)(canvasSize.Width - margin.LeftAndRight), (float)(canvasSize.Height - margin.TopAndBottom));
            var startX = this.scaleService.State.X1;
            var endX = this.scaleService.State.X2;

            if (this.dataService.ItemCount == 1)
            {
                // Draw point for single data item
                foreach (var item in this.dataService.GetItems(startX, endX))
                {
                    if (item != null)
                    {
                        var x = this.scaleService.ToScreen(Axis.X, item.X);
                        var y = this.scaleService.ToScreen(Axis.Y, item.Y);
                        if (IsVisible(canvasSize, margin, x, y))
                        {
                            drawing.Circle(this.State.LineColor, margin.Left + x, canvasSize.Height - margin.Bottom - y, 4);
                        }                        
                    }
                }
            }
            else
            {
                IDataItem prevItem = null;
                // Draw lines
                foreach (var item in this.dataService.GetItems(startX, endX))
                {
                    if (item != null && prevItem != null)
                    {
                        var x1 = this.scaleService.ToScreen(Axis.X, prevItem.X);
                        var y1 = this.scaleService.ToScreen(Axis.Y, prevItem.Y);

                        var x2 = this.scaleService.ToScreen(Axis.X, item.X);
                        var y2 = this.scaleService.ToScreen(Axis.Y, item.Y);

                        if (ClipLine(canvasSize, margin, ref x1, ref y1, ref x2, ref y2))
                        {
                            drawing.Line(this.State.LineColor, margin.Left + x1, canvasSize.Height - margin.Bottom - y1, margin.Left + x2, canvasSize.Height - margin.Bottom - y2, clip);
                        }
                    }
                    prevItem = item;
                }
            }
        }

        /// <summary>
        /// Clip line to canvas and margin. (Currently only checks that both point are in area)
        /// </summary>
        /// <param name="canvasSize">canvas size to check boundaries</param>
        /// <param name="margin">margin to clip inside margin</param>
        /// <param name="x1">ref x start line</param>
        /// <param name="y1">y start line</param>
        /// <param name="x2">x end of line</param>
        /// <param name="y2">y end of line</param>
        /// <returns></returns>
        private static bool ClipLine(Size canvasSize, IMargin margin, ref double x1, ref double y1, ref double x2, ref double y2)
        {
            return IsVisible(canvasSize, margin, x1, y1) || IsVisible(canvasSize, margin, x2, y2);
        }

        private static bool IsVisible(Size canvasSize, IMargin margin, double x, double y)
        {
            return x >= 0 && x <= canvasSize.Width - margin.Right - margin.Left &&
                y >= 0 && y <= canvasSize.Height - margin.Top - margin.Bottom;
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}
