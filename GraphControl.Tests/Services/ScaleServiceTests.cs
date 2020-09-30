using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphControl.Core.Exceptions;
using GraphControl.Tests.Unitilies;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Definitions;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Services;

namespace GraphControl.Tests.Services
{
    [TestClass()]
    public class ScaleServiceTests
    {
        [TestMethod()]
        public void ScaleServiceCreateNullTest()
        {
            Assert.ThrowsException<InvalidArgumentException>(() => new ScaleService(null, null, null));
        }

        [TestMethod()]
        public void ToScreenTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            double x = new DateTime(1985, 6, 23).Ticks / TimeSpan.TicksPerMillisecond;
            Assert.IsTrue(scaleService.ToScreen(Axis.X, 0) == (0 - scaleService.State.X1) * scaleService.State.ScaleX);
            Assert.IsTrue(scaleService.ToScreen(Axis.X, x) == (x - scaleService.State.X1) * scaleService.State.ScaleX);

            Assert.IsTrue(scaleService.ToScreen(Axis.Y, 0) == (0 - scaleService.State.Y1) * scaleService.State.ScaleY);
            Assert.IsTrue(scaleService.ToScreen(Axis.Y, .5) == (.5 - scaleService.State.Y1) * scaleService.State.ScaleY);
        }

        [TestMethod()]
        public void ToScreenXTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            double x = new DateTime(1985, 6, 23).Ticks / TimeSpan.TicksPerMillisecond;
            Assert.IsTrue(scaleService.ToScreen(Axis.X, 0) == (0 - scaleService.State.X1) * scaleService.State.ScaleX);
            Assert.IsTrue(scaleService.ToScreen(Axis.X, x) == (x - scaleService.State.X1) * scaleService.State.ScaleX);
        }

        [TestMethod()]
        public void ToScreenYTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            Assert.IsTrue(scaleService.ToScreen(Axis.Y, 0) == (0 - scaleService.State.Y1) * scaleService.State.ScaleY);
            Assert.IsTrue(scaleService.ToScreen(Axis.Y, .5) == (.5 - scaleService.State.Y1) * scaleService.State.ScaleY);
        }

        [TestMethod()]
        public void ScaleToScreenTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            double x = new DateTime(1985, 6, 23).Ticks / TimeSpan.TicksPerMillisecond;
            Assert.IsTrue(scaleService.ScaleToScreen(Axis.X, 0) == 0);
            Assert.IsTrue(scaleService.ScaleToScreen(Axis.X, x) == x * scaleService.State.ScaleX);

            Assert.IsTrue(scaleService.ScaleToScreen(Axis.Y, 0) == 0);
            Assert.IsTrue(scaleService.ScaleToScreen(Axis.Y, .5) == .5 * scaleService.State.ScaleY);
        }

        [TestMethod()]
        public void ScaleToScreenXTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            double x = new DateTime(1985, 6, 23).Ticks / TimeSpan.TicksPerMillisecond;
            Assert.IsTrue(scaleService.ScaleToScreenX(0) == 0);
            Assert.IsTrue(scaleService.ScaleToScreenX(x) == x * scaleService.State.ScaleX);
        }

        [TestMethod()]
        public void ScaleToScreenYTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            Assert.IsTrue(scaleService.ScaleToScreenY(0) == 0);
            Assert.IsTrue(scaleService.ScaleToScreenY(.5) == .5 * scaleService.State.ScaleY);
        }

        [TestMethod()]
        public void ToDataTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            double x = new DateTime(1985, 6, 23).Ticks / TimeSpan.TicksPerMillisecond;
            Assert.IsTrue(scaleService.ToData(Axis.X, 0) == 0 / scaleService.State.ScaleX + scaleService.State.X1);
            Assert.IsTrue(scaleService.ToData(Axis.X, x) == x / scaleService.State.ScaleX + scaleService.State.X1);

            Assert.IsTrue(scaleService.ToData(Axis.Y, 0) == 0 / scaleService.State.ScaleY + scaleService.State.Y1);
            Assert.IsTrue(scaleService.ToData(Axis.Y, .5) == .5 / scaleService.State.ScaleY + scaleService.State.Y1);
        }

        [TestMethod()]
        public void ToDataXTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            double x = new DateTime(1985, 6, 23).Ticks / TimeSpan.TicksPerMillisecond;
            Assert.IsTrue(scaleService.ToDataX(0) == 0 / scaleService.State.ScaleX + scaleService.State.X1);
            Assert.IsTrue(scaleService.ToDataX(x) == x / scaleService.State.ScaleX + scaleService.State.X1);
        }

        [TestMethod()]
        public void ToDataYTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);

            Assert.IsTrue(scaleService.ToDataY(0) == 0 / scaleService.State.ScaleY + scaleService.State.Y1);
            Assert.IsTrue(scaleService.ToDataY(.5) == .5 / scaleService.State.ScaleY + scaleService.State.Y1);
        }

        [TestMethod()]
        public void ScaleToDataTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            double x = new DateTime(1985, 6, 23).Ticks / TimeSpan.TicksPerMillisecond;
            Assert.IsTrue(scaleService.ScaleToData(Axis.X, 0) == 0 / scaleService.State.ScaleX);
            Assert.IsTrue(scaleService.ScaleToData(Axis.X, x) == x / scaleService.State.ScaleX);

            Assert.IsTrue(scaleService.ScaleToData(Axis.Y, 0) == 0 / scaleService.State.ScaleY);
            Assert.IsTrue(scaleService.ScaleToData(Axis.Y, .5) == .5 / scaleService.State.ScaleY);
        }

        [TestMethod()]
        public void ScaleToDataXTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            double x = new DateTime(1985, 6, 23).Ticks / TimeSpan.TicksPerMillisecond;
            Assert.IsTrue(scaleService.ScaleToDataX(0) == 0 / scaleService.State.ScaleX);
            Assert.IsTrue(scaleService.ScaleToDataX(x) == x / scaleService.State.ScaleX);
        }

        [TestMethod()]
        public void ScaleToDataYTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);

            Assert.IsTrue(scaleService.ScaleToDataY(0) == 0 / scaleService.State.ScaleY);
            Assert.IsTrue(scaleService.ScaleToDataY(.5) == .5 / scaleService.State.ScaleY);
        }

        ////[TestMethod()]
        ////public void CanvasSizeChangedTest()
        ////{

        ////}

        [TestMethod()]
        public void SetStepTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);

            double step = 0;
            step = 0;       scaleService.SetStep(Axis.X, step); Assert.IsTrue(step == scaleService.State.StepX);
            step = 10000;   scaleService.SetStep(Axis.X, step); Assert.IsTrue(step == scaleService.State.StepX);

            step = 0;       scaleService.SetStep(Axis.Y, step); Assert.IsTrue(step == scaleService.State.StepY);
            step = 0.125;   scaleService.SetStep(Axis.Y, step); Assert.IsTrue(step == scaleService.State.StepY);
        }

        [TestMethod()]
        public void SetStepXTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);

            double step = 0;
            step = 0;       scaleService.SetStepX(step); Assert.IsTrue(step == scaleService.State.StepX);
            step = 10000;   scaleService.SetStepX(step); Assert.IsTrue(step == scaleService.State.StepX);
        }

        [TestMethod()]
        public void SetStepYTest()
        {
            TestFactory.CreateBaseServices(null, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);

            double step = 0;
            step = 0;       scaleService.SetStepY(step); Assert.IsTrue(step == scaleService.State.StepY);
            step = 0.125;   scaleService.SetStepY(step); Assert.IsTrue(step == scaleService.State.StepY);
        }

        ////[TestMethod()]
        ////public void UpdateScaleTest()
        ////{

        ////}

        ////[TestMethod()]
        ////public void UpdateMarginTest()
        ////{

        ////}

        [TestMethod()]
        public void ZoomAndUnzoomTest()
        {
            var controller = TestFactory.CreateApplicationController();
            TestFactory.CreateBaseServices(controller, null,
                out IGridState gridState, out IDataDrawState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);

            int iterations = 100000;
            var state = new ScaleState(scaleService.State);
            for (int i = 0; i < iterations; i++)
            {
                scaleService.Zoom(-120);
            }
            for (int i = 0; i < iterations && !state.Equals(scaleService.State); i++)
            {
                scaleService.Zoom(-120);
            }
            Assert.IsTrue(state.Equals(scaleService.State), $"State before zooming/unzooming not the same as before");
        }

        ////[TestMethod()]
        ////public void ZoomTest1()
        ////{

        ////}

        ////[TestMethod()]
        ////public void ZoomTest2()
        ////{

        ////}

        ////[TestMethod()]
        ////public void MoveTest()
        ////{

        ////}

        ////[TestMethod()]
        ////public void IsItemVisibleTest()
        ////{

        ////}

        ////[TestMethod()]
        ////public void IsItemsVisibleTest()
        ////{

        ////}
    }
}