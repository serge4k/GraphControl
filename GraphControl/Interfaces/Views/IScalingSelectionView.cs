﻿using System.Drawing;

namespace GraphControlCore.Interfaces.Views
{
    public interface IScalingSelectionView : IDrawingView
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