using GraphControlCore.Interfaces;
using GraphControlCore.Structs;
using System;

namespace GraphControlCore.Events
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