using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphControl.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphControl.Core.Exceptions;
using GraphControl.Tests.Unitilies;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Models;

namespace GraphControl.Core.Services.Tests
{
    [TestClass()]
    public class ScaleServiceTests
    {
        [TestMethod()]
        public void ScaleServiceCreateNullTest()
        {
            ;
            Assert.ThrowsException<InvalidArgumentException>(() => new ScaleService(null, null, null));
        }

        [TestMethod()]
        public void ToScreenTest()
        {

        }

        [TestMethod()]
        public void ToScreenXTest()
        {

        }

        [TestMethod()]
        public void ToScreenYTest()
        {

        }

        [TestMethod()]
        public void ScaleToScreenTest()
        {

        }

        [TestMethod()]
        public void ScaleToScreenXTest()
        {

        }

        [TestMethod()]
        public void ScaleToScreenYTest()
        {

        }

        [TestMethod()]
        public void ToDataTest()
        {

        }

        [TestMethod()]
        public void ToDataXTest()
        {

        }

        [TestMethod()]
        public void ToDataYTest()
        {

        }

        [TestMethod()]
        public void ScaleToDataTest()
        {

        }

        [TestMethod()]
        public void ScaleToDataXTest()
        {

        }

        [TestMethod()]
        public void ScaleToDataYTest()
        {

        }

        [TestMethod()]
        public void CanvasSizeChangedTest()
        {

        }

        [TestMethod()]
        public void SetStepTest()
        {

        }

        [TestMethod()]
        public void SetStepXTest()
        {

        }

        [TestMethod()]
        public void SetStepYTest()
        {

        }

        [TestMethod()]
        public void UpdateScaleTest()
        {

        }

        [TestMethod()]
        public void UpdateMarginTest()
        {

        }

        [TestMethod()]
        public void ZoomAndUnzoomTest()
        {
            var controller = TestFactory.CreateApplicationController();
            TestFactory.CreateBaseServices(controller, null,
                out IGridState gridState, out IGraphState graphState,
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

        [TestMethod()]
        public void ZoomTest1()
        {

        }

        [TestMethod()]
        public void ZoomTest2()
        {

        }

        [TestMethod()]
        public void MoveTest()
        {

        }

        [TestMethod()]
        public void IsItemVisibleTest()
        {

        }

        [TestMethod()]
        public void IsItemsVisibleTest()
        {

        }
    }
}