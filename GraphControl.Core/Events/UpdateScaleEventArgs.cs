using System;
using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Events
{
    public class UpdateScaleEventArgs : EventArgs
    {
        public IDrawOptions DrawOptions { get; private set; }

        public UpdateScaleEventArgs(IDrawOptions drawOptions)
        {
            this.DrawOptions = drawOptions;
        }
    }
}
