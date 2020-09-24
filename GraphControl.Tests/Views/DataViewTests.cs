using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphControl.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphControl.Tests.Services;
using GraphControl.Core.Structs;
using GraphControl.Tests.Unitilies;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Factory;
using System.Threading;
using GraphControl.Core.Services;
using GraphControl.Core.Utilities;
using GraphControl.Core.Interfaces;

namespace GraphControl.Tests.Views
{
    [TestClass()]
    public class DataViewTests
    {
        [TestMethod()]
        public void CreateWithNullsTest()
        {
            Assert.ThrowsException<InvalidArgumentException>(() => new DataView(null, null));
        }

        [TestMethod()]
        public void CreateTest()
        {
            var obj = TestDataView.Create();
            Assert.IsTrue(obj is IDataView, $"Type of obj is {obj.GetType().ToString()} but not IDataView");
        }

        [TestMethod()]
        public void DrawWithNullParamsTest()
        {
            var view = TestDataView.Create();
            Assert.ThrowsException<InvalidArgumentException>(() => view.Draw(null, new DrawOptions(new Size(0,0), true, true, null), null));
        }

        [TestMethod()]
        public void DrawNoDataTest()
        {
            var view = TestDataView.Create();
            var size = new Size(800, 600);
            TestDrawingWrapper drawing = null;
            drawing = StartCreateAndDrawDataLines(0, false, false) as TestDrawingWrapper;
            Assert.IsTrue(drawing.Lines.Count == 0, $"lines count is {drawing.Lines.Count} more than 0");
            drawing.Dispose();
            Assert.IsTrue(drawing.Flushes.Count == 1, $"Flushes count = {drawing.Flushes.Count}");

            // Draw without data
            StartCreateAndDrawDataLines(0, true, false);
        }

        [TestMethod()]
        public void DrawAllLinesDataTest()
        {
            StartCreateAndDrawDataLines(1001, false, true);
        }

        private static IDrawing StartCreateAndDrawDataLines(int pointsNumber, bool drawInBitmap, bool runProvider)
        {
            var linesNumber = pointsNumber - 1;
            var linesToDraw = 0;
            IDataProviderService provider = null;

            TestDrawingWrapper drawingTester = null;
            DrawingBuffer buffer = null;
            Drawing2DWrapper drawingBitmap = null;
            IDrawing drawing = null;
            var size = new Size(800, 600);
            if (drawInBitmap)
            {
                size = new Size(8000, 4500);
                buffer = new DrawingBuffer(size);
                drawingBitmap = new Drawing2DWrapper(buffer.Graphics);
                drawing = drawingBitmap;
            }
            else
            {
                drawingTester = new TestDrawingWrapper(100);
                drawing = drawingTester;
            }

            IScaleService scaleService = null;
            IDataService dataService = null;
            var controller = GraphControlFactory.CreateController();
            IBufferedDrawingService bufferedDrawingService = new BufferedDrawingService();
            int receivedPoints = 0;
            try
            {
                // Create all services
                provider = TestSinusDataProviderService.Create(pointsNumber);
                var view = TestDataView.Create(controller, provider, bufferedDrawingService);
                
                var margin = new Margin(100, 5, 5, 60);
                var options = new DataDrawOptions(size, true, true, null, new DataDrawState());
                scaleService = controller.GetInstance<IScaleService>();
                scaleService.UpdateScale(options); // to prepare scaling service without presenter

                dataService = controller.GetInstance<IDataService>();
                
                dataService.DataUpdated += (sender, e) =>
                {
                    scaleService.UpdateScale(options);
                    drawingTester?.Reset();
                    view.Draw(drawing, options, margin);
                    receivedPoints += e.Items.Count;
                };

                if (pointsNumber > 0)
                {
                    provider.Run();

                    // Wait to draw at least test points
                    while (receivedPoints < pointsNumber)
                    {
                        Thread.Sleep(0);
                    }
                }
                else
                {
                    view.Draw(drawing, options, margin);
                }

                linesToDraw = dataService.GetItems(scaleService.State.X1, scaleService.State.X2).Count();
            }
            finally
            {
                bufferedDrawingService.Dispose();
            }

            Assert.IsTrue(bufferedDrawingService.LastQueueOverflow.Ticks == 0, $"drawing queue overflow, last time: {bufferedDrawingService.LastQueueOverflow.ToLongTimeString()}");

            Assert.IsTrue(linesToDraw == pointsNumber,
                $"dataService.GetItems({new DateTime((long)scaleService.State.X1 * TimeSpan.TicksPerMillisecond).ToString("HH:mm:ss.fff")}"
                + $", {new DateTime((long)scaleService.State.X1 * TimeSpan.TicksPerMillisecond).ToString("HH:mm:ss.fff")}) {linesToDraw} ({dataService.ItemCount}) != generated points {pointsNumber}");

            if (!drawInBitmap)
            {
                Assert.IsTrue(drawingTester.Lines.Count >= linesNumber, $"lines count is {drawingTester.Lines.Count} less than {linesNumber}");
            }

            return drawing;
        }

        [TestMethod()]
        public void DrawWith10kInBitmapPerformanceTest()
        {
            StartCreateAndDrawDataLines(10001, true, true);
        }
    }
}