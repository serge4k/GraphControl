using GraphControl.Tests.Unitilies;
using GraphControl.Core.Factory;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Views;
using GraphControl.Core.Interfaces;

namespace GraphControl.Tests.Views
{
    internal class TestDataView
    {
        public static IDataView Create()
        {
            return TestDataView.Create(null);
        }

        public static IDataView Create(int testPointsNumber)
        {
            TestFactory.CreateBaseServices(testPointsNumber, out IGridState gridState, out IGraphState graphState, out IDataService dataService, out IScaleService scaleService);
            return new DataView(graphState, scaleService, dataService);
        }

        public static IDataView Create(IDataProviderService provider)
        {
            TestFactory.CreateBaseServices(provider,
                out IGridState gridState, out IGraphState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            return new DataView(graphState, scaleService, dataService);
        }

        public static IDataView Create(IApplicationController applicationController, IDataProviderService provider, IBufferedDrawingService bufferedDrawingService)
        {
            TestFactory.CreateBaseServices(applicationController, provider, 
                out IGridState gridState, out IGraphState graphState, 
                out IItemFormatter itemFormatter, out IMargin margin, 
                out IDataService dataService, out IScaleService scaleService, ref bufferedDrawingService);
            return new DataView(graphState, scaleService, dataService);
        }
    }
}
