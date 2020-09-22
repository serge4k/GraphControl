using System;
using GraphControl.Events;
using GraphControl.Interfaces;
using GraphControl.Interfaces.Presenters;
using GraphControl.Interfaces.Services;
using GraphControl.Interfaces.Views;
using GraphControl.Structs;

namespace GraphControl.Presenters
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
