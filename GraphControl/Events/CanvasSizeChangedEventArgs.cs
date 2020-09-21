using System;
using GraphControl.Structs;

namespace GraphControl.Events
{
    public class ControlSizeChangedEventArgs : EventArgs
    {
        public Size CanvasSize;

        public ControlSizeChangedEventArgs(Size canvasSize)
        {
            this.CanvasSize = canvasSize;
        }
    }
}