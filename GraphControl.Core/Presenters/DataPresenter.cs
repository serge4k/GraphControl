using System;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Views;

namespace GraphControl.Core.Presenters
{
    public class DataPresenter : IDataPresenter
    {
        #region Public properties
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
        #endregion

        #region Private fields
        private readonly IDataView view;
        private readonly IDataDrawState state;
        private readonly IDataService dataService;
        #endregion

        #region Contructors
        public DataPresenter(IDataView dataView, IDataDrawState state, IDataService dataService)
        {
            this.view = dataView;
            this.state = state;
            this.dataService = dataService;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Draws the view
        /// </summary>
        /// <param name="drawing">drawing wrapper</param>
        /// <param name="options">drawing options</param>
        /// <param name="margin">drawing margin</param>
        public void Draw(IDrawing drawing, IDrawOptions options, IMargin margin)
        {
            this.view.Draw(drawing, new DataDrawOptions(options, this.state), margin);
        }
        #endregion
    }
}
