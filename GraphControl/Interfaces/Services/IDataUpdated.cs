
using System;
using GraphControl.Events;

namespace GraphControl.Interfaces.Services
{
    public interface IDataUpdated
    {
        event EventHandler<DataUpdatedEventArgs> DataUpdated;
    }
}
