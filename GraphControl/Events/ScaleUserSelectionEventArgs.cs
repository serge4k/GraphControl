using System;
using System.Drawing;
using GraphControl.Definitions;

namespace GraphControl.Events
{
    public class ScaleUserSelectionEventArgs : EventArgs
    {
        public MouseButtons Button { get; set; }

        public Point Location { get; set; }

        public int WheelDelta { get; set; }

        public bool ShiftPressed { get; internal set; }

        public ScaleUserSelectionEventArgs(MouseButtons button, Point location, int wheelDelta, bool shiftPressed)
        {
            this.Button = button;
            this.Location = location;
            this.WheelDelta = wheelDelta;
            this.ShiftPressed = shiftPressed;
        }
    }
}