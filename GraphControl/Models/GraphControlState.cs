using GraphControlCore.Interfaces.Models;

namespace GraphControlCore.Models
{
    public class GraphControlState : IGraphControlState
    {
        public IGraphControlFormState ControlFormState { get; set; }

        public IScaleState ScaleState { get; set; }

        public IBackgroundState BackgroundState { get; set; }

        public GraphControlState(IGraphControlFormState controlFormState, IScaleState scaleState, IBackgroundState backgroundState)
        {
            this.ControlFormState = controlFormState;
            this.ScaleState = scaleState;
            this.BackgroundState = backgroundState;
        }
    }
}
