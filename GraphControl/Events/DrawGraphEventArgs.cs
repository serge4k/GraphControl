using GraphControl.Interfaces;
using GraphControl.Structs;
using System;

namespace GraphControl.Events
{
    public class DrawGraphEventArgs : EventArgs
    {
        public IDrawing Drawing { get; private set; }

        public DrawOptions DrawOptions { get; private set; }

        public DrawGraphEventArgs(IDrawing drawing, DrawOptions drawOptions)
        {
            this.Drawing = drawing;
            this.DrawOptions = drawOptions;
        }
    }
}