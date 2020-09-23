using System;

namespace GraphControl.Core.Interfaces.Models
{
    public interface IGraphControlState
    {
        IGraphControlFormState ControlFormState { get; set; }

        IScaleState ScaleState { get; set; }

        IBackgroundState BackgroundState { get; set; }
    }
}
