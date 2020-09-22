using GraphControlCore.Exceptions;
using GraphControlCore.Interfaces;
using GraphControlCore.Interfaces.Models;
using GraphControlCore.Interfaces.Presenters;
using GraphControlCore.Interfaces.Services;
using GraphControlCore.Interfaces.Views;
using GraphControlCore.Models;
using GraphControlCore.Presenters;
using GraphControlCore.Services;
using GraphControlCore.Utilities;
using GraphControlCore.Views;

namespace GraphControlCore.Factory
{
    public static class GraphControlFactory
    {
        public static IApplicationController CreateController()
        {
            var controller = new ApplicationController(new DependInjectWrapper());
            return controller.RegisterInstance<IApplicationController>(controller);
        }

        public static void RegisterInstances(IApplicationController applicationController, IGraphControlFormView formView, IGraphControlView controlView)
        {
            if (applicationController == null || formView == null)
            {
                throw new GraphControlException("parameter is null");
            }

            var margin = formView.GraphMargin;
            var itemFormatter = formView.ItemFormatter;
            
            applicationController.RegisterService<IBufferedDrawingService, BufferedDrawingService>();

            IBackgroundState backgroundState;
            IGridState gridState;
            IScaleState scaleState;
            IGraphState graphState;
            IGraphControlFormState graphControlFormState;
            CreateStateInstancees(applicationController, 
                out backgroundState, out gridState, out scaleState, out graphState, out graphControlFormState);

            IDataService dataService;
            IScaleService scaleService;
            IBufferedDrawingService bufferedDrawingService;
            CreateServiceInstances(applicationController, 
                margin, scaleState, 
                out dataService, out scaleService, out bufferedDrawingService);

            IBackgroundPresenter backgroundPresenter;
            IGridPresenter gridPresenter;
            IDataPresenter dataPresenter;
            IScalingSelectionView scalingView;
            CreateViewInstances(applicationController, 
                controlView, itemFormatter, 
                backgroundState, gridState, graphState, 
                dataService, scaleService,
                formView.UserBackgroundView, formView.UserGridView, formView.UserDataView, formView.UserScalingSelectionView,
                out backgroundPresenter, out gridPresenter, out dataPresenter, out scalingView);

            CreatePresenterInstances(applicationController, 
                formView, controlView, scalingView,
                graphControlFormState, 
                scaleService, bufferedDrawingService, 
                backgroundPresenter, gridPresenter, dataPresenter);
        }

        private static void CreatePresenterInstances(IApplicationController applicationController, 
            IGraphControlFormView formView, IGraphControlView controlView, IScalingSelectionView scalingView,
            IGraphControlFormState graphControlFormState, 
            IScaleService scaleService, IBufferedDrawingService bufferedDrawingService, 
            IBackgroundPresenter backgroundPresenter, IGridPresenter gridPresenter, IDataPresenter dataPresenter)
        {
            var scalingPresenter = new ScalingSelectionPresenter(scalingView, controlView, scaleService);
            applicationController.RegisterInstance<IScalingSelectionPresenter>(scalingPresenter);

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

            var graphControlFormPresenter = new GraphControlFormPresenter(
                applicationController,
                formView,
                graphControlFormState,
                graphControlPresenter
                );
            applicationController.RegisterInstance<IGraphControlFormPresenter>(graphControlFormPresenter);
        }

        private static void CreateViewInstances(IApplicationController applicationController, 
            IGraphControlView controlView, IItemFormatter itemFormatter, 
            IBackgroundState backgroundState, IGridState gridState, IGraphState graphState, 
            IDataService dataService, IScaleService scaleService, 
            IBackgroundView userBackGroundView, IGridView userGridView, IDataView userDataView, IScalingSelectionView userScalingSelectionView, 
            out IBackgroundPresenter backgroundPresenter, out IGridPresenter gridPresenter, out IDataPresenter dataPresenter, out IScalingSelectionView scalingView)
        {
            var backgroundView = userBackGroundView ?? new BackgroundView(backgroundState);
            backgroundPresenter = new BackgroundPresenter(backgroundView);
            applicationController.RegisterInstance<IBackgroundPresenter>(backgroundPresenter);

            var gridView = userGridView ?? new GridView(gridState, scaleService, itemFormatter);
            gridPresenter = new GridPresenter(gridView);
            applicationController.RegisterInstance<IGridPresenter>(gridPresenter);

            var dataView = userDataView ?? new DataView(graphState, scaleService, dataService);
            dataPresenter = new DataPresenter(dataView, dataService);
            applicationController.RegisterInstance<IDataPresenter>(dataPresenter);

            scalingView = userScalingSelectionView ?? new ScalingSelectionView();

            // Register IGraphControlView here
            applicationController.RegisterInstance<IGraphControlView>(controlView);
        }

        private static void CreateServiceInstances(IApplicationController applicationController, 
            IMargin margin, IScaleState scaleState, 
            out IDataService dataService, out IScaleService scaleService, out IBufferedDrawingService bufferedDrawingService)
        {
            dataService = new DataService();
            applicationController.RegisterInstance<IDataService>(dataService);

            scaleService = new ScaleService(scaleState, dataService, margin);
            applicationController.RegisterInstance<IScaleService>(scaleService);

            bufferedDrawingService = new BufferedDrawingService();
            applicationController.RegisterInstance<IBufferedDrawingService>(bufferedDrawingService);
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
    }
}
