﻿using System;

namespace GraphControlCore.Events
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