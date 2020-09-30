using GraphControl.Core.Interfaces;
using GraphControl.Core.Structs;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Services
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
                throw new InvalidArgumentException("parameter is null");
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

        public override int GetHashCode()
        {
            return this.Margin.GetHashCode() ^ 2147483647
                + this.X1.GetHashCode() ^ 2147483647 
                + this.X2.GetHashCode() ^ 2147483647 
                + this.Y1.GetHashCode() ^ 2147483647 
                + this.Y2.GetHashCode() ^ 2147483647
                + this.ScaleX.GetHashCode() ^ 2147483647 
                + this.ScaleY.GetHashCode() ^ 2147483647
                + this.StepX.GetHashCode() ^ 2147483647 
                + this.StepY.GetHashCode() ^ 2147483647;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IScaleState))
                return false;

            return Equals((IScaleState)obj);
        }

        public bool Equals(IScaleState other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Margin.Equals(other.Margin)
                && this.X1.Equals(other.X1)
                && this.X2.Equals(other.X2)
                && this.Y1.Equals(other.Y1)
                && this.Y2.Equals(other.Y2)
                && this.ScaleX.Equals(other.ScaleX)
                && this.ScaleY.Equals(other.ScaleY)
                && this.StepX.Equals(other.StepX)
                && this.StepY.Equals(other.StepY);
                ////&& this.FitByX.Equals(other.FitByX) // TODO remove
                ////&& this.FitByY.Equals(other.FitByY);
        }

        public static bool operator ==(ScaleState obj1, IScaleState obj2)
        {
            if (obj1 == null)
            {
                return false;
            }
            return obj1.Equals(obj2);
        }

        public static bool operator !=(ScaleState obj1, IScaleState obj2)
        {
            if (obj1 == null)
            {
                return false;
            }
            return !obj1.Equals(obj2);
        }
    }
}
