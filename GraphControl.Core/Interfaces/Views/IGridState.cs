using System.Drawing;

namespace GraphControl.Core.Interfaces.Views
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

        /// <summary>
        /// Grid label text padding
        /// </summary>
        IMargin LabelPadding { get; set; }

        /// <summary>
        /// Text formatter
        /// </summary>
        IItemFormatter ItemFormatter { get; set; }

    }
}
