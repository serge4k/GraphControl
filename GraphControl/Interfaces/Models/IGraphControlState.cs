using System;

namespace GraphControl.Interfaces.Models
{
    public interface IGraphControlState
    {
        IGraphControlFormState ControlFormState { get; set; }

        IScaleState ScaleState { get; set; }

        IBackgroundState BackgroundState { get; set; }
    }
}
