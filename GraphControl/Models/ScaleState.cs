using GraphControl.Definitions;
using GraphControl.Interfaces.Models;
using GraphControl.Interfaces;
using GraphControl.Structs;
using GraphControl.Exceptions;

namespace GraphControl.Models
{
    public class ScaleState : IScaleState
    {
        /// <summary>
        /// Graph margin
        /// </summary>
        public IMargin Margin { get; set; }

        /// <summary>
        /// Data related X value from start visible area
        /// </summary>
        public double X1 { get; set; }

        /// <summary>
        /// Data related X value from end visible area
        /// </summary>
        public double X2 { get; set; }

        /// <summary>
        /// Data related Y value from start visible area
        /// </summary>
        public double Y1 { get; set; }

        /// <summary>
        /// Data related Y value from end visible area
        /// </summary>
        public double Y2 { get; set; }

        /// <summary>
        /// Multiplier for X
        /// </summary>
        public double ScaleX { get; set; }

        /// <summary>
        /// Multiplier for X
        /// </summary>
        public double ScaleY { get; set; }

        /// <summary>
        /// Last division value by X
        /// </summary>
        public double StepX { get; set; }

        /// <summary>
        /// Last division value by Y
        /// </summary>
        public double StepY { get; set; }

        public ScaleState()
        {
            this.Margin = new Margin();
            this.X1 = -10;
            this.X2 = 10;
            this.Y1 = -1.5;
            this.Y2 = 1.5;
            this.ScaleX = 1;
            this.ScaleY = 1;
            this.StepX = 1;
            this.StepY = 1;
        }

        public ScaleState(IScaleState state)
        {
            if (state == null)
            {
                throw new GraphControlException("parameter is null");
            }
            this.Margin = new Margin(state.Margin);
            this.X1 = state.X1;
            this.X2 = state.X2;
            this.Y1 = state.Y1;
            this.Y2 = state.Y2;
            this.ScaleX = state.ScaleX;
            this.ScaleY = state.ScaleY;
            this.StepX = state.StepX;
            this.StepY = state.StepY;
        }
    }
}
