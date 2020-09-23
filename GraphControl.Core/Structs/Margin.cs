using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;

namespace GraphControl.Core.Structs
{
    public class Margin : IMargin
    {
        /// <summary>
        /// Screen related left graphic offet (to provide space for grid marks)
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Screen related top graphic offet
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Screen related right graphic offet
        /// </summary>
        public double Right { get; set; }

        /// <summary>
        /// Screen related bottom graphic offset (to provide space for grid marks)
        /// </summary>
        public double Bottom { get; set; }

        /// <summary>
        /// Calculated LeftAndRight
        /// </summary>
        public double LeftAndRight
        {
            get
            {
                return this.Right + this.Left;
            }
        }

        /// <summary>
        /// Calculated TopAndBottom
        /// </summary>
        public double TopAndBottom
        {
            get
            {
                return this.Bottom + this.Top;
            }
        }


        public Margin() : this(50, 5, 5, 40)
        {

        }

        public Margin(double left, double top, double right, double bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public Margin(IMargin margin)
        {
            if (margin == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            this.Left = margin.Left;
            this.Top = margin.Top;
            this.Right = margin.Right;
            this.Bottom = margin.Bottom;
        }
    }
}
