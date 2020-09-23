using System;
using GraphControl.Core.Events;

namespace GraphControl.Core.Interfaces
{
    public interface IGraphDataArrayProvider
    {
        event EventHandler<GraphDataArrayEventArgs> OnGraphDataArrayReceived;
    }
}
