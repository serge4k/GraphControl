using System;
using GraphControl.Events;

namespace GraphControl.Interfaces
{
    public interface IGraphDataArrayProvider
    {
        event EventHandler<GraphDataArrayEventArgs> OnGraphDataArrayReceived;
    }
}
