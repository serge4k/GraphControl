using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Views;
using System;

namespace GraphControl.Core.Events
{
    public class DrawGraphEventArgs : EventArgs
    {
        public IDrawing Drawing { get; private set; }

        public IDrawOptions DrawOptions { get; private set; }

        public DrawGraphEventArgs(IDrawing drawing, IDrawOptions drawOptions)
        {
            this.Drawing = drawing;
            this.DrawOptions = drawOptions;
        }
    }
}