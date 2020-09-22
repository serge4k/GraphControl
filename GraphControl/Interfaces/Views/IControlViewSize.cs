using System;
using GraphControlCore.Events;

namespace GraphControlCore.Interfaces.Views
{
    public interface IControlViewSize
    {
        event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;
    }
}
