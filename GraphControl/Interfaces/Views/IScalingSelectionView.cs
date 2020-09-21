using System.Drawing;

namespace GraphControl.Interfaces.Views
{
    public interface IScalingSelectionView : IDrawingView
    {
        Color MovingPenColor { get; set; }
        Point? MovingPos { get; set; }
        Point? MovingStart { get; set; }
        Point? ScalingPos { get; set; }
        Point? ScalingStart { get; set; }
        bool ZoomIncrease { get; set; }
        Color ZoomInPenColor { get; set; }
        Color ZoomOutPenColor { get; set; }
    }
}