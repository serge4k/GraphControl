using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphControl.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace GraphControl.Tests.Views
{
    [TestClass()]
    public class GridViewTests
    {
        [TestMethod()]
        public void CreateWithNullsTest()
        {
            Assert.ThrowsException<InvalidArgumentException>(() => new GridView(null, null, null, null));
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
            Assert.ThrowsException<InvalidArgumentException>(() => gridView.Draw(null, new DrawOptions(), null));
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
                var options = new DrawOptions(size, true, true);
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
            var gridView = TestGridView.Create(TestSinusDataProviderService.Create(linesNumber));
            var size = new Size(800, 600);
            TestDrawingWrapper drawing = null;
            using (drawing = new TestDrawingWrapper())
            {
                var margin = new Margin(100, 5, 5, 60);
                var options = new DrawOptions(size, true, true);
                gridView.Draw(drawing, options, margin);
            }
            Assert.IsTrue(drawing.Lines.Count >= 10);
            Assert.IsTrue(drawing.Texts.Count > 5);
            Assert.IsTrue(drawing.MeasureTexts.Count > 0);
            Assert.IsTrue(drawing.Flushes.Count == 1, $"Flushes count = {drawing.Flushes.Count}");
        }

        [TestMethod()]
        public void ShowMethodShouldThrowNotImplementedExceptionTest()
        {
            var gridView = TestGridView.Create();
            Assert.ThrowsException<NotImplementedException>(() => gridView.Show());
        }
    }
}