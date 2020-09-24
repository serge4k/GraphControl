using System;
using GraphControl.Core.Events;

namespace GraphControl.Core.Interfaces
{
    public interface IDrawGraph
    {
        /// <summary>
        /// Draw graph event
        /// </summary>
        event EventHandler<DrawGraphEventArgs> DrawGraph;
    }
}
