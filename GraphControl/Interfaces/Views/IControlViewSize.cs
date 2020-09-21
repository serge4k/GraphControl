using System;
using GraphControl.Events;
using GraphControl.Structs;

namespace GraphControl.Interfaces.Views
{
    public interface IControlViewSize
    {
        event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;
    }
}
