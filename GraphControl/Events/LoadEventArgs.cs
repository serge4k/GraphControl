using System;
using GraphControl.Structs;

namespace GraphControl.Events
{
    public class LoadEventArgs : EventArgs
    {
        public Rect Rect { get; private set; }

        public LoadEventArgs(Rect rect)
        {
            this.Rect = rect;
        }
    }
}
