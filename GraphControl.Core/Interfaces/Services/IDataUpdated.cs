
using System;
using GraphControl.Core.Events;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IDataUpdated
    {
        /// <summary>
        /// Data updated core event to notify about new DataItems block was received
        /// </summary>
        event EventHandler<DataUpdatedEventArgs> DataUpdated;
    }
}
