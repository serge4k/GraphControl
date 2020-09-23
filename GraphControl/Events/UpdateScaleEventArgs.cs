using System;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Events
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
