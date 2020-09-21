using System;
using GraphControl.Structs;

namespace GraphControl.Events
{
    public class LoadEventArgs : EventArgs
    {
        public Rect Rect;

        public LoadEventArgs(Rect rect)
        {
            this.Rect = rect;
        }
    }
}
