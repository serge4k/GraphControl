using System;
using GraphControlCore.Structs;

namespace GraphControlCore.Events
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
