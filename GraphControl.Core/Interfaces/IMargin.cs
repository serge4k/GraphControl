using System;

namespace GraphControl.Core.Interfaces
{
    public interface IMargin : IEquatable<IMargin>
    {
        /// <summary>
        /// Screen related left graphic offet (to provide space for grid marks)
        /// </summary>
        double Left { get; set; }

        /// <summary>
        /// Screen related top graphic offet
        /// </summary>
        double Top { get; set; }

        /// <summary>
        /// Screen related right graphic offet
        /// </summary>
        double Right { get; set; }

        /// <summary>
        /// Screen related bottom graphic offset (to provide space for grid marks)
        /// </summary>
        double Bottom { get; set; }

        /// <summary>
        /// Calculated LeftAndRight
        /// </summary>
        double LeftAndRight { get; }

        /// <summary>
        /// Calculated TopAndBottom
        /// </summary>
        double TopAndBottom { get; }
    }
}
