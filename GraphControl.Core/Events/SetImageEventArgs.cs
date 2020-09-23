using System;
using System.Drawing;

namespace GraphControl.Core.Events
{
    public class SetImageEventArgs : EventArgs
    {
        public Bitmap Bitmap {get; private set;}

        public SetImageEventArgs(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
        }
    }
}