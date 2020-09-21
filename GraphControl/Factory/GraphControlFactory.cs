using GraphControl.Interfaces;
using GraphControl.Interfaces.Models;
using GraphControl.Interfaces.Presenters;
using GraphControl.Interfaces.Services;
using GraphControl.Interfaces.Views;
using GraphControl.Models;
using GraphControl.Presenters;
using GraphControl.Services;
using GraphControl.Utilites;
using GraphControl.Views;

namespace GraphControl.Factory
{
    public class GraphControlFactory
    {
        public static GraphControlFactory Factory
        {
            get
            {
                return new GraphControlFactory();
            }
        }

        public IApplicationController CreateController()
        {
            var controller = new ApplicationController(new DependInjectWrapper());
            return controller.RegisterInstance<IApplicationController>(controller);
        }

        public void RegisterInstances(IApplicationController applicationController, IGraphControlFormView formView, IGraphControlView controlView, IItemFormatter itemFormatter, IMargin margin, bool bufferedDrawing)
        {
            applicationController.RegisterService<IBufferedDrawingService, BufferedDrawingService>();

            var backgroundState = new BackgroundState();
            applicationController.RegisterInstance<IBackgroundState>(backgroundState);

            var gridState = new GridState();
            applicationController.RegisterInstance<IGridState>(gridState);

            var scaleState = new ScaleState();
            applicationController.RegisterInstance<IScaleState>(scaleState);

            var graphState = new GraphState();
            applicationController.RegisterInstance<IGraphState>(graphState);

            var dataService = new DataService();
            applicationController.RegisterInstance<IDataService>(dataService);

            var scaleService = new ScaleService(scaleState, dataService, margin);
            applicationController.RegisterInstance<IScaleService>(scaleService);

            var backgroundView = new BackgroundView(backgroundState);
            var backgroundPresenter = new BackgroundPresenter(backgroundView);
            applicationController.RegisterInstance<IBackgroundPresenter>(backgroundPresenter);

            var gridView = new GridView(gridState, scaleService, itemFormatter);
            var gridPresenter = new GridPresenter(gridView);
            applicationController.RegisterInstance<IGridPresenter>(gridPresenter);

            var dataView = new DataView(graphState, scaleService, dataService);
            var dataPresenter = new DataPresenter(dataView, dataService);
            applicationController.RegisterInstance<IDataPresenter>(dataPresenter);

            var scalingView = new ScalingSelectionView();

            // Register IGraphControlView here
            applicationController.RegisterInstance<IGraphControlView>(controlView);

            var scalingPresenter = new ScalingSelectionPresenter(scalingView, controlView, scaleService);
            applicationController.RegisterInstance<IScalingSelectionPresenter>(scalingPresenter);

            BufferedDrawingService bufferedDrawingService = null;
            if (bufferedDrawing)
            {
                bufferedDrawingService = new BufferedDrawingService();
            }
            applicationController.RegisterInstance<IBufferedDrawingService>(bufferedDrawingService);

            var graphControlPresenter = new GraphControlPresenter(applicationController,
                controlView,
                scaleService,
                bufferedDrawingService,
                backgroundPresenter,
                gridPresenter,
                dataPresenter,
                scalingPresenter
                );
            applicationController.RegisterInstance<IGraphControlPresenter>(graphControlPresenter);

            var graphControlFormState = new GraphControlFormState();
            applicationController.RegisterInstance<IGraphControlFormState>(graphControlFormState);

            var graphControlFormPresenter = new GraphControlFormPresenter(
                applicationController,
                formView,
                graphControlFormState,
                scaleService,
                graphControlPresenter,
                controlView
                );
            applicationController.RegisterInstance<IGraphControlFormPresenter>(graphControlFormPresenter);
        }
    }
}
