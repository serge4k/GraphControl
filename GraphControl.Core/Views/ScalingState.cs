using System.Drawing;
using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Views
{
    public class ScalingState : IScalingState
    {
        public Point? MovingStart { get; set; }

        public Point? MovingPosition { get; set; }

        public Point? ScalingStart { get; set; }

        public Point? ScalingPosition { get; set; }

        public bool ZoomIncrease { get; set; }

        public Color MovingPenColor { get; set; }

        public Color ZoomInPenColor { get; set; }

        public Color ZoomOutPenColor { get; set; }
    }
}
