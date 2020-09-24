using System;
using GraphControl.Core.Events;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IControlViewSize
    {
        /// <summary>
        /// Control size was changed forwarding event
        /// </summary>
        event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;
    }
}
