using System;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Events
{
    public class ControlSizeChangedEventArgs : EventArgs
    {
        public Size CanvasSize { get; private set; }

        public ControlSizeChangedEventArgs(Size canvasSize)
        {
            this.CanvasSize = canvasSize;
        }
    }
}