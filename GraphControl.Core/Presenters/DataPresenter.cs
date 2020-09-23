﻿using System;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Presenters
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
