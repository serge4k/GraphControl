
using System;
using GraphControl.Core.Events;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IDataUpdated
    {
        event EventHandler<DataUpdatedEventArgs> DataUpdated;
    }
}
