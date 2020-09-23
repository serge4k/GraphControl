﻿using GraphControl.Core.Exceptions;
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

        public override int GetHashCode()
        {
            return this.Left.GetHashCode() ^ 137 
                + this.Top.GetHashCode() ^ 137
                + this.Right.GetHashCode() ^ 137 
                + this.Bottom.GetHashCode() ^ 137;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IMargin))
                return false;

            return Equals((IMargin)obj);
        }

        public bool Equals(IMargin other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Left.Equals(other.Left)
                && this.Top.Equals(other.Top)
                && this.Right.Equals(other.Right)
                && this.Bottom.Equals(other.Bottom);
        }

        public static bool operator ==(Margin obj1, IMargin obj2)
        {
            if (obj1 == null)
            {
                return false;
            }
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Margin obj1, IMargin obj2)
        {
            if (obj1 == null)
            {
                return false;
            }
            return !obj1.Equals(obj2);
        }
    }
}
