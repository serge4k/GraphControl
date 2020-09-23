using GraphControl.Core.Factory;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Views;
using GraphControl.Tests.Unitilies;

namespace GraphControl.Tests.Views
{
    internal class TestGridView
    {
        public static IGridView Create()
        {
            return TestGridView.Create(null, null);
        }

        public static IGridView Create(IApplicationController applicationController, IDataProviderService provider)
        {
            TestFactory.CreateBaseServices(applicationController, provider,
                out IGridState gridState, out IGraphState graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out IDataService dataService, out IScaleService scaleService);
            return new GridView(gridState, scaleService, itemFormatter, margin);
        }
    }
}
