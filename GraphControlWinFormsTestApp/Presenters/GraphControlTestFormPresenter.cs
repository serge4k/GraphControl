﻿using GraphControlCore.Interfaces.Services;
using GraphControlCore.Interfaces;
using GraphControlCore.Presenters;
using GraphControlWinFormsTestApp.Interfaces;

namespace GraphControlWinFormsTestApp.Presenters
{
    internal class GraphControlTestFormPresenter : BasePresenter<IGraphControlTestFormView>
    {
        private readonly IDataProviderService dataProviderService;

        public GraphControlTestFormPresenter(IApplicationController controller, IGraphControlTestFormView view, IDataProviderService dataProviderService) : base(controller, view)
        {
            this.dataProviderService = dataProviderService;
        }

        public override void Run()
        {
            this.View.RegisterDataProvider(this.dataProviderService);
            this.dataProviderService.Run();
            this.View.ShowView();
        }
    }
}
