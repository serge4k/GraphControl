using System;
using GraphControl.Events;
using GraphControl.Structs;

namespace GraphControl.Interfaces
{
    public interface ICanvas
    {
        Size ControlSize { get; }

        event EventHandler<DrawGraphEventArgs> DrawGraph;

        event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;
    }
}
