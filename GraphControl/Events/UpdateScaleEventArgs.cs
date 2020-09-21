using System;
using GraphControl.Structs;

namespace GraphControl.Events
{
    public class UpdateScaleEventArgs : EventArgs
    {
        public DrawOptions DrawOptions { get; private set; }

        public UpdateScaleEventArgs(DrawOptions drawOptions)
        {
            this.DrawOptions = drawOptions;
        }
    }
}
