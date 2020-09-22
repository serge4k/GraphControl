using System;
using GraphControl.Events;

namespace GraphControl.Interfaces.Views
{
    public interface IControlViewSize
    {
        event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;
    }
}
