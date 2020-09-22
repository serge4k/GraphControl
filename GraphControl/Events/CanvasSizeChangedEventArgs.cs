using System;
using GraphControlCore.Structs;

namespace GraphControlCore.Events
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