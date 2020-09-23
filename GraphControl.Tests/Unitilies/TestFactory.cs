using GraphControl.Tests.Views;
using GraphControl.Core.Factory;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphControl.Tests.Services;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Models;
using GraphControl.Core.Services;

namespace GraphControl.Tests.Unitilies
{
    internal static class TestFactory
    {
        public static void CreateBaseServices(int testPointNumber, out IGridState gridState, out IGraphState graphState, out IDataService dataService, out IScaleService scaleService)
        {
            var provider = TestSinusDataProviderService.Create(testPointNumber);
            TestFactory.CreateBaseServices(null, provider,
                out gridState, out graphState,
                out IItemFormatter itemFormatter, out IMargin margin,
                out dataService, out scaleService);
            provider.Run();
        }

        public static void CreateBaseServices(IApplicationController applicationController, IDataProviderService provider,
            out IGridState gridState, out IGraphState graphState,
            out IItemFormatter itemFormatter, out IMargin margin,
            out IDataService dataService, out IScaleService scaleService)
        {
            applicationController = applicationController ?? GraphControlFactory.CreateController();

            IBufferedDrawingService bufferedDrawingService = null;
            TestFactory.CreateBaseServices(applicationController, provider,
                out gridState, out graphState,
                out itemFormatter, out margin,
                out dataService, out scaleService, ref bufferedDrawingService);
        }

        public static void CreateBaseServices(IApplicationController applicationController, IDataProviderService provider,
            out IGridState gridState, out IGraphState graphState,
            out IItemFormatter itemFormatter, out IMargin margin,
            out IDataService dataService, out IScaleService scaleService, ref IBufferedDrawingService bufferedDrawingService)
        {
            var formView = new TestGraphControlFormView();
            margin = formView.GraphMargin;
            itemFormatter = formView.ItemFormatter;
            var labelMargin = formView.LabelMargin;

            // Set property and Reset()
            formView.LabelMargin = labelMargin;
            formView.Reset();

            TestFactory.CreateStateInstancees(applicationController,
                out IBackgroundState backgroundState, out gridState, out IScaleState scaleState, out graphState, out IGraphControlFormState graphControlFormState);

            TestFactory.CreateServiceInstances(applicationController,
                margin, scaleState,
                out dataService, out scaleService, ref bufferedDrawingService);

            if (provider != null)
            {
                dataService.RegisterDataProvider(provider);
            }
        }

        private static void CreateStateInstancees(IApplicationController applicationController, 
            out IBackgroundState backgroundState, out IGridState gridState, out IScaleState scaleState, out IGraphState graphState, out IGraphControlFormState graphControlFormState)
        {
            backgroundState = new BackgroundState();
            applicationController.RegisterInstance<IBackgroundState>(backgroundState);

            gridState = new GridState();
            applicationController.RegisterInstance<IGridState>(gridState);

            scaleState = new ScaleState();
            applicationController.RegisterInstance<IScaleState>(scaleState);

            graphState = new GraphState();
            applicationController.RegisterInstance<IGraphState>(graphState);

            graphControlFormState = new GraphControlFormState();
            applicationController.RegisterInstance<IGraphControlFormState>(graphControlFormState);
        }


        private static void CreateServiceInstances(IApplicationController applicationController,
            IMargin margin, IScaleState scaleState,
            out IDataService dataService, out IScaleService scaleService, ref IBufferedDrawingService bufferedDrawingService)
        {
            dataService = new DataService();
            applicationController.RegisterInstance<IDataService>(dataService);

            scaleService = new ScaleService(scaleState, dataService, margin);
            applicationController.RegisterInstance<IScaleService>(scaleService);

            if (bufferedDrawingService == null)
            {
                bufferedDrawingService = new BufferedDrawingService();
            }            
            applicationController.RegisterInstance<IBufferedDrawingService>(bufferedDrawingService);
        }

    }
}
