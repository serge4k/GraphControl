﻿using System;
using GraphControl.Core.Events;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Interfaces
{
    public interface ICanvas
    {
        Size ControlSize { get; }

        event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;
    }
}
