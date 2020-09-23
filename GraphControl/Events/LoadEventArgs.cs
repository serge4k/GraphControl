using System;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Events
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
