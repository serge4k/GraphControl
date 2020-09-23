using System.Drawing;

namespace GraphControl.Core.Interfaces.Models
{
    public interface IGridState
    {
        /// <summary>
        /// Min distance bteween grid lines
        /// </summary>
        double MinGridLineDistance { get; set; }

        /// <summary>
        /// Axis Pen color
        /// </summary>
        Color AxeColor { get; set; }

        /// <summary>
        /// Grid lines Pen color
        /// </summary>
        Color GridColor { get; set; }

        /// <summary>
        /// Text color for X axis
        /// </summary>
        Color TextXColor { get; set; }

        /// <summary>
        /// Text color for Y axis
        /// </summary>
        Color TextYColor { get; set; }
    }
}
