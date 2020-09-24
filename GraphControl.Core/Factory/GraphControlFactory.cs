using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Models;
using GraphControl.Core.Presenters;
using GraphControl.Core.Services;
using GraphControl.Core.Utilities;
using GraphControl.Core.Views;

namespace GraphControl.Core.Factory
{
    public static class GraphControlFactory
    {
        /// <summary>
        /// Creates ApplicationController to provide dependency injection controller
        /// </summary>
        /// <returns>IApplicationController interface</returns>
        public static IApplicationController CreateController()
        {
            var controller = new ApplicationController(new DependInjectWrapper());
            return controller.RegisterInstance<IApplicationController>(controller);
        }

        /// <summary>
        /// Creates and registers core object instances for GraphControl
        /// </summary>
        /// <param name="applicationController">IApplicationController interface (Refer to the CreateController() method)</param>
        /// <param name="formView">IGraphControlFormView interface (Refer to the GraphControlWinForms project example)</param>
        /// <param name="controlView">inner IGraphControlView interface</param>
        public static void RegisterInstances(IApplicationController applicationController, IGraphControlFormView formView, IGraphControlView controlView)
        {
            if (applicationController == null || formView == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            
            applicationController.RegisterService<IBufferedDrawingService, BufferedDrawingService>();

            CreateStateInstances(applicationController,
                formView.ItemFormatter,
                formView.LabelMargin,
                out IBackgroundState backgroundState, 
                out IGridState gridState, 
                out IScaleState scaleState, 
                out IDataDrawState graphState,
                out IScalingState scalingState,
                out IGraphControlFormState graphControlFormState);

            CreateServiceInstances(applicationController,
                formView.GraphMargin, scaleState,
                out IDataService dataService, 
                out IScaleService scaleService, 
                out IBufferedDrawingService bufferedDrawingService);

            CreateViewInstances(applicationController,
                controlView,
                dataService, 
                scaleService,
                formView.UserBackgroundView, 
                formView.UserGridView, 
                formView.UserDataView, 
                formView.UserScalingSelectionView,
                backgroundState,
                gridState,
                graphState,
                out IBackgroundPresenter backgroundPresenter, 
                out IGridPresenter gridPresenter, 
                out IDataPresenter dataPresenter, 
                out IScalingSelectionView scalingView);

            CreatePresenterInstances(applicationController, 
                formView, 
                controlView, 
                scalingView,
                graphControlFormState,
                scalingState,
                dataService, 
                scaleService, 
                bufferedDrawingService, 
                backgroundPresenter, 
                gridPresenter, 
                dataPresenter);
        }

        private static void CreatePresenterInstances(IApplicationController applicationController, 
            IGraphControlFormView formView, 
            IGraphControlView controlView, 
            IScalingSelectionView scalingView,
            IGraphControlFormState graphControlFormState,
            IScalingState scalingState,
            IDataService dataService, 
            IScaleService scaleService, 
            IBufferedDrawingService bufferedDrawingService, 
            IBackgroundPresenter backgroundPresenter, 
            IGridPresenter gridPresenter, 
            IDataPresenter dataPresenter)
        {
            var scalingPresenter = new ScalingSelectionPresenter(scalingView, scalingState, controlView, scaleService);
            applicationController.RegisterInstance<IScalingSelectionPresenter>(scalingPresenter);

            var graphControlPresenter = new GraphControlPresenter(applicationController,
                controlView,
                dataService,
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
            applicationController.RegisterInstance<GraphControlFormPresenter>(graphControlFormPresenter);
        }

        private static void CreateViewInstances(IApplicationController applicationController, 
            IGraphControlView controlView, 
            IDataService dataService, 
            IScaleService scaleService, 
            IBackgroundView userBackgroundView, 
            IGridView userGridView, 
            IDataView userDataView, 
            IScalingSelectionView userScalingSelectionView,
            IBackgroundState userBackgroundState,
            IGridState userGridState,
            IDataDrawState userDataDrawState,
            out IBackgroundPresenter backgroundPresenter, 
            out IGridPresenter gridPresenter, 
            out IDataPresenter dataPresenter, 
            out IScalingSelectionView scalingView)
        {
            var backgroundView = userBackgroundView ?? new BackgroundView();
            backgroundPresenter = new BackgroundPresenter(backgroundView, userBackgroundState);
            applicationController.RegisterInstance<IBackgroundPresenter>(backgroundPresenter);

            var gridView = userGridView ?? new GridView(scaleService);
            gridPresenter = new GridPresenter(gridView, userGridState);
            applicationController.RegisterInstance<IGridPresenter>(gridPresenter);

            var dataView = userDataView ?? new DataView(scaleService, dataService);
            dataPresenter = new DataPresenter(dataView, userDataDrawState, dataService);
            applicationController.RegisterInstance<IDataPresenter>(dataPresenter);

            scalingView = userScalingSelectionView ?? new ScalingView();

            // Register IGraphControlView here
            applicationController.RegisterInstance<IGraphControlView>(controlView);
        }

        private static void CreateServiceInstances(IApplicationController applicationController, 
            IMargin margin, 
            IScaleState scaleState, 
            out IDataService dataService, 
            out IScaleService scaleService, 
            out IBufferedDrawingService bufferedDrawingService)
        {
            dataService = new DataService();
            applicationController.RegisterInstance<IDataService>(dataService);

            scaleService = new ScaleService(scaleState, dataService, margin);
            applicationController.RegisterInstance<IScaleService>(scaleService);

            bufferedDrawingService = new BufferedDrawingService();
            applicationController.RegisterInstance<IBufferedDrawingService>(bufferedDrawingService);
        }

        private static void CreateStateInstances(IApplicationController applicationController,
            IItemFormatter itemFormatter,
            IMargin labelMargin,
            out IBackgroundState backgroundState, 
            out IGridState gridState, 
            out IScaleState scaleState, 
            out IDataDrawState graphState,
            out IScalingState scalingState,
            out IGraphControlFormState graphControlFormState)
        {
            backgroundState = new BackgroundState();
            applicationController.RegisterInstance<IBackgroundState>(backgroundState);

            gridState = new GridState();
            gridState.LabelPadding = labelMargin;
            gridState.ItemFormatter = itemFormatter;
            applicationController.RegisterInstance<IGridState>(gridState);

            scaleState = new ScaleState();
            applicationController.RegisterInstance<IScaleState>(scaleState);

            graphState = new DataDrawState();
            applicationController.RegisterInstance<IDataDrawState>(graphState);

            scalingState = new ScalingState();
            applicationController.RegisterInstance<IScalingState>(scalingState);

            graphControlFormState = new GraphControlFormState();
            applicationController.RegisterInstance<IGraphControlFormState>(graphControlFormState);
        }
    }
}
