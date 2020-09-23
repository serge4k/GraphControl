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
            Assert.ThrowsException<InvalidArgumentException>(() => new DataView(null, null, null));
        }

        [TestMethod()]
        public void CreateTest()
        {
            var obj = TestDataView.Create();
            Assert.IsTrue(obj is IDataView, $"Type of obj is {obj.GetType().ToString()}");
        }

        [TestMethod()]
        public void DrawNullTest()
        {
            var view = TestDataView.Create();
            Assert.ThrowsException<InvalidArgumentException>(() => view.Draw(null, new DrawOptions(), null));
        }

        [TestMethod()]
        public void DrawNoDataTest()
        {
            var view = TestDataView.Create();
            var size = new Size(800, 600);
            TestDrawingWrapper drawing = null;
            using (drawing = new TestDrawingWrapper())
            {
                var margin = new Margin(100, 5, 5, 60);
                var options = new DrawOptions(size, true, true, null);
                view.Draw(drawing, options, margin);
            }
            Assert.IsTrue(drawing.Lines.Count == 0, $"lines count is {drawing.Lines.Count} more than 0");
            Assert.IsTrue(drawing.Flushes.Count == 1, $"Flushes count = {drawing.Flushes.Count}");
        }

        [TestMethod()]
        public void DrawAllLinesDataTest()
        {
            DrawWith10kLinesDataTest(1000, false);
        }

        private static void DrawWith10kLinesDataTest(int linesNumber, bool drawInBitmap)
        {
            var pointsNumber = linesNumber + 1;
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
            try
            {
                IDataView view;
                // Create all services
                using (provider = TestSinusDataProviderService.Create(pointsNumber))
                {
                    view = TestDataView.Create(controller, provider, bufferedDrawingService);
                    provider.Run();
                }

                var margin = new Margin(100, 5, 5, 60);
                var options = new DrawOptions(size, true, true, null);
                scaleService = controller.GetInstance<IScaleService>();
                scaleService.UpdateScale(options); // to prepare scaling service without presenter

                dataService = controller.GetInstance<IDataService>();
                linesToDraw = dataService.GetItems(scaleService.State.X1, scaleService.State.X2).Count();
                scaleService.Zoom(120);
                view.Draw(drawing, options, margin);
            }
            finally
            {
                drawingBitmap?.Dispose();
                drawingTester?.Dispose();
                bufferedDrawingService.Dispose();
            }

            Assert.IsTrue(bufferedDrawingService.LastQueueOverflow.Ticks == 0, $"drawing queue overflow, last time: {bufferedDrawingService.LastQueueOverflow.ToLongTimeString()}");

            Assert.IsTrue(linesToDraw == pointsNumber,
                $"dataService.GetItems({new DateTime((long)scaleService.State.X1 * TimeSpan.TicksPerMillisecond).ToShortTimeString()}"
                + $", {new DateTime((long)scaleService.State.X1 * TimeSpan.TicksPerMillisecond).ToShortTimeString()}) {linesToDraw} ({dataService.ItemCount}) != generated points {pointsNumber}");

            if (!drawInBitmap)
            {
                Assert.IsTrue(drawingTester.Lines.Count >= linesNumber, $"lines count is {drawingTester.Lines.Count} less than {linesNumber}");

                Assert.IsTrue(drawingTester.Flushes.Count == 1, $"Flushes count = {drawingTester.Flushes.Count}");
            }            
        }

        [TestMethod()]
        public void ShowMethodShouldThrowNotImplementedExceptionTest()
        {
            var view = TestGridView.Create();
            Assert.ThrowsException<NotImplementedException>(() => view.Show());
        }

        [TestMethod()]
        public void DrawWith10kInBitmapPerformanceTest()
        {
            DrawWith10kLinesDataTest(10000, true);
        }
    }
}