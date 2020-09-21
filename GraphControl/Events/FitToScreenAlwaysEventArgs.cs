using System;

namespace GraphControl.Events
{
    public class FitToScreenAlwaysEventArgs : EventArgs
    {
        public bool FitToScreenAlways { get; private set; }

        public FitToScreenAlwaysEventArgs(bool fitToScreenAlways)
        {
            this.FitToScreenAlways = fitToScreenAlways;
        }
    }
}