
using System;
using GraphControlCore.Events;

namespace GraphControlCore.Interfaces.Services
{
    public interface IDataUpdated
    {
        event EventHandler<DataUpdatedEventArgs> DataUpdated;
    }
}
