using System.Drawing;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IScalingState
    {
        Color MovingPenColor { get; set; }

        Point? MovingPosition { get; set; }

        Point? MovingStart { get; set; }

        Point? ScalingPosition { get; set; }

        Point? ScalingStart { get; set; }

        bool ZoomIncrease { get; set; }

        Color ZoomInPenColor { get; set; }

        Color ZoomOutPenColor { get; set; }
    }
}