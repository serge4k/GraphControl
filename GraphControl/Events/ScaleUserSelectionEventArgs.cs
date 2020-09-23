﻿using System;
using System.Drawing;
using GraphControl.Core.Definitions;

namespace GraphControl.Core.Events
{
    public class ScaleUserSelectionEventArgs : EventArgs
    {
        public MouseButton Button { get; set; }

        public Point Location { get; set; }

        public int WheelDelta { get; set; }

        public bool ShiftPressed { get; internal set; }

        public ScaleUserSelectionEventArgs(MouseButton button, Point location, int wheelDelta, bool shiftPressed)
        {
            this.Button = button;
            this.Location = location;
            this.WheelDelta = wheelDelta;
            this.ShiftPressed = shiftPressed;
        }
    }
}