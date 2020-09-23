namespace GraphControl.Core.Interfaces.Models
{
    public interface IScaleState
    {
        /// <summary>
        /// Graph margin
        /// </summary>
        IMargin Margin { get; set; } 

        /// <summary>
        /// Data related X value from start visible area
        /// </summary>
        double X1 { get; set; }

        /// <summary>
        /// Data related X value from end visible area
        /// </summary>
        double X2 { get; set; }

        /// <summary>
        /// Data related Y value from start visible area
        /// </summary>
        double Y1 { get; set; }

        /// <summary>
        /// Data related Y value from end visible area
        /// </summary>
        double Y2 { get; set; }

        /// <summary>
        /// Multiplier for X
        /// </summary>
        double ScaleX { get; set; }

        /// <summary>
        /// Multiplier for X
        /// </summary>
        double ScaleY { get; set; }

        /// <summary>
        /// Last division value by X
        /// </summary>
        double StepX { get; set; }

        /// <summary>
        /// Last division value by Y
        /// </summary>
        double StepY { get; set; }
    }
}
