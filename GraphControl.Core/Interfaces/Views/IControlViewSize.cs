using System;
using GraphControl.Core.Events;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IControlViewSize
    {
        event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;
    }
}
