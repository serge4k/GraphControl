using System.Drawing;
using GraphControlCore.Interfaces.Models;

namespace GraphControlCore.Models
{
    public class GridState : IGridState
    {
        private const double minGridLineDistanceDefault = 25;

        /// <summary>
        /// Min distance bteween grid lines
        /// </summary>
        public double MinGridLineDistance { get; set; }

        /// <summary>
        /// Axis Pen color
        /// </summary>
        public Color AxeColor { get; set; }

        /// <summary>
        /// Grid lines Pen color
        /// </summary>
        public Color GridColor { get; set; }

        /// <summary>
        /// Text color for X axis
        /// </summary>
        public Color TextXColor { get; set; }
        
        /// <summary>
        /// Text color for Y axis
        /// </summary>
        public Color TextYColor { get; set; }

        public GridState()
        {
            // Fill defaults
            this.MinGridLineDistance = minGridLineDistanceDefault;
            this.AxeColor = Color.FromArgb(90, 90, 90);
            this.GridColor = Color.FromArgb(180, 180, 180);
            this.TextXColor = Color.FromArgb(0, 220, 0);
            this.TextYColor = Color.FromArgb(0, 0, 222);
        }
    }
}
