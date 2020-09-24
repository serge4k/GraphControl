using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphControl.Core.Views;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Models;
using GraphControl.Core.Utilities;
using GraphControl.Core.Services;
using GraphControl.Core.Factory;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Tests.Views;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Structs;
using GraphControl.Tests.Unitilies;
using GraphControl.Tests.Services;
using GraphControl.Core.Interfaces;

namespace GraphControl.Tests.Views
{
    [TestClass()]
    public class GridViewTests
    {
        [TestMethod()]
        public void CreateWithNullsTest()
        {
            Assert.ThrowsException<InvalidArgumentException>(() => new GridView(null));
        }

        [TestMethod()]
        public void CreateTest()
        {
            var gridView = TestGridView.Create();
            Assert.IsTrue(gridView is IGridView, $"Type of GridView is {gridView.GetType().ToString()}");
        }

        [TestMethod()]
        public void DrawNullTest()
        {
            var gridView = TestGridView.Create();
            Assert.ThrowsException<InvalidArgumentException>(() => gridView.Draw(null, new DrawOptions(new Size(0, 0), true, true, null), null));
        }

        [TestMethod()]
        public void DrawNoDataTest()
        {
            var gridView = TestGridView.Create();
            var size = new Size(800, 600);
            TestDrawingWrapper drawing = null;
            using (drawing = new TestDrawingWrapper())
            {
                var margin = new Margin(100, 5, 5, 60);
                var options = new GridDrawOptions(size, true, true, null, new GridState());
                gridView.Draw(drawing, options, margin);
            }
            Assert.IsTrue(drawing.Lines.Count > 0);
            Assert.IsTrue(drawing.Texts.Count > 0);
            Assert.IsTrue(drawing.MeasureTexts.Count > 0);
            Assert.IsTrue(drawing.Flushes.Count == 1, $"Flushes count = {drawing.Flushes.Count}");
        }

        [TestMethod()]
        public void DrawWithDataTest()
        {
            var linesNumber = 1000;
            var pointsNumber = linesNumber + 1;
            TestDrawingWrapper drawing = StartDataGeneration(TestSinusDataProviderService.Create(pointsNumber), pointsNumber, linesNumber);
            Assert.IsTrue(drawing.Lines.Count >= 9, $"drawn lines for axis {drawing.Lines.Count} less than generated {9}");
            Assert.IsTrue(drawing.Texts.Count > 5);
            Assert.IsTrue(drawing.MeasureTexts.Count > 0);
            Assert.IsTrue(drawing.Flushes.Count == 1, $"Flushes count = {drawing.Flushes.Count}");
        }

        private static TestDrawingWrapper StartDataGeneration(IDataProviderService dataProviderService, int pointsNumber, int linesNumber)
        {
            var applicationController = GraphControlFactory.CreateController();
            var gridView = TestGridView.Create(applicationController, dataProviderService);
            var size = new Size(800, 600);
            TestDrawingWrapper drawing = null;
            int receivedPoints = 0;
            using (drawing = new TestDrawingWrapper())
            {
                var margin = new Margin(100, 5, 5, 60);
                var options = new GridDrawOptions(size, true, true, null, new GridState());

                var scaleService = applicationController.GetInstance<IScaleService>();
                applicationController.GetInstance<IDataService>().DataUpdated += (sender, e) =>
                {
                    scaleService.UpdateScale(options);
                    drawing.Reset();
                    gridView.Draw(drawing, options, margin);
                    receivedPoints += e.Items.Count;
                };

                dataProviderService.Run();

                while (receivedPoints < pointsNumber)
                {
                    Thread.Sleep(0);
                }
            }
            dataProviderService.Dispose();
            return drawing;
        }

        [TestMethod()]
        public void DataExtremeRangesTest()
        {
            int linesNumber = 10;
            var pointsNumber = linesNumber + 1;
            StartDataGeneration(new TestRangesDataProviderService((uint)pointsNumber), pointsNumber, linesNumber);
        }
    }
}