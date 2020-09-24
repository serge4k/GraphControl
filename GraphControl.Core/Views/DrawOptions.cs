using System;
using System.Collections.Generic;
using System.Linq;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Views
{
    public class DrawOptions : IDrawOptions, IEquatable<DrawOptions>
    {
        /// <summary>
        /// Canvas area width and heigth
        /// </summary>
        public Size CanvasSize { get; private set; }

        /// <summary>
        /// Fix by X option
        /// </summary>
        public bool FitToX { get; private set; }

        /// <summary>
        /// Fix by X option
        /// </summary>
        public bool FitToY { get; private set; }

        /// <summary>
        /// Contains new items or null when all items should be drawn
        /// </summary>
        public ICollection<IDataItem> NewItems { get; private set; }

        public bool DrawOnlyNewData
        {
            get
            {
                return !this.FitToX && !this.FitToY && this.NewItems != null && this.NewItems.Count > 0;
            }
        }

        public DrawOptions(Size canvasSize, bool fitToX, bool fitToY, ICollection<IDataItem> dataItems)
        {
            this.CanvasSize = canvasSize;
            this.FitToX = fitToX;
            this.FitToY = fitToY;
            this.NewItems = dataItems;
        }

        public DrawOptions(IDrawOptions options)
        {
            if (options == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            this.CanvasSize = options.CanvasSize;
            this.FitToX = options.FitToX;
            this.FitToY = options.FitToY;
            this.NewItems = options.NewItems;
        }

        public override int GetHashCode()
        {
            return this.CanvasSize.GetHashCode() ^ 137
                + this.FitToX.GetHashCode() ^ 137
                + this.FitToY.GetHashCode() ^ 137
                + (this.NewItems != null ? this.NewItems.GetHashCode() ^ 137 : 0);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DrawOptions))
                return false;

            return Equals((DrawOptions)obj);
        }

        public bool Equals(DrawOptions other)
        {
            if (other == null)
            {
                return false;
            }
            return this.CanvasSize.Equals(other.CanvasSize)
                && this.FitToX.Equals(other.FitToX)
                && this.FitToY.Equals(other.FitToY)
                && ((this.NewItems == null && other.NewItems == null) || this.NewItems != null ? this.NewItems.SequenceEqual(other.NewItems) : false);
        }

        public static bool operator ==(DrawOptions options1, DrawOptions options2)
        {
            if (options1 == null)
            {
                return false;
            }
            return options1.Equals(options2);
        }

        public static bool operator !=(DrawOptions options1, DrawOptions options2)
        {
            if (options1 == null)
            {
                return false;
            }
            return !options1.Equals(options2);
        }
    }

    public class DrawOptions<TState> : DrawOptions
    {
        public TState State { get; private set; }

        public DrawOptions(IDrawOptions options, TState state)
            : base(options)
        {
            this.State = state;
        }

        public DrawOptions(Size canvasSize, bool fitToX, bool fitToY, ICollection<IDataItem> dataItems, TState state)
            : base(canvasSize, fitToX, fitToY, dataItems)
        {
            this.State = state;
        }
    }
}
