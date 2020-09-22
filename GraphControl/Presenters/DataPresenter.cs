using System;
using GraphControlCore.Events;
using GraphControlCore.Interfaces;
using GraphControlCore.Interfaces.Presenters;
using GraphControlCore.Interfaces.Services;
using GraphControlCore.Interfaces.Views;
using GraphControlCore.Structs;

namespace GraphControlCore.Presenters
{
    public class DataPresenter : IDataPresenter
    {
        public event EventHandler<DataUpdatedEventArgs> DataUpdated
        {
            add
            {
                this.dataService.DataUpdated += value;
            }
            remove
            {
                this.dataService.DataUpdated -= value;
            }
        }

        private readonly IDataView view;
        private readonly IDataService dataService;
        
        public DataPresenter(IDataView dataView, IDataService dataService)
        {
            this.view = dataView;
            this.dataService = dataService;
        }

        public void Draw(IDrawing drawing, DrawOptions options, IMargin margin)
        {
            this.view.Draw(drawing, options, margin);
        }
    }
}
