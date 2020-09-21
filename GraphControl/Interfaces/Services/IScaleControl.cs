﻿using System.Drawing;

namespace GraphControl.Interfaces.Services
{
    public interface IScaleControl
    {
        void Zoom(Rectangle rectangle, bool increase);

        void Zoom(Point location, int wheelDelta);

        void Zoom(int wheelDelta);

        void Move(int offsetX, int offsetY);
    }
}
