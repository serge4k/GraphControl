using System;
using GraphControlCore.Structs;

namespace GraphControlCore.Events
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
