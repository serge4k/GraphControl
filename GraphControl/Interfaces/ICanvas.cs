using System;
using GraphControlCore.Events;
using GraphControlCore.Structs;

namespace GraphControlCore.Interfaces
{
    public interface ICanvas
    {
        Size ControlSize { get; }

        event EventHandler<DrawGraphEventArgs> DrawGraph;

        event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;
    }
}
