using System;
using GraphControlCore.Events;

namespace GraphControlCore.Interfaces
{
    public interface IGraphDataArrayProvider
    {
        event EventHandler<GraphDataArrayEventArgs> OnGraphDataArrayReceived;
    }
}
