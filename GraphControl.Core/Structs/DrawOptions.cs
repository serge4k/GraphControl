using System;
using System.Collections.Generic;
using System.Linq;
using GraphControl.Core.Interfaces.Models;

namespace GraphControl.Core.Structs
{
    public struct DrawOptions : IEquatable<DrawOptions>
    {
        public Size CanvasSize { get; private set; }

        public bool FitToX { get; private set; }

        public bool FitToY { get; private set; }

        public ICollection<IDataItem> NewItems { get; private set; }

        public bool DrawOnlyNewData
        {
            get
            {
                return !this.FitToX && !this.FitToY && this.NewItems != null && this.NewItems.Count > 0;
            }
        }

        ////public DrawOptions(Size canvasSize) : this(canvasSize, false, false, null)
        ////{
        ////}

        public DrawOptions(Size canvasSize, bool fitToX, bool fitToY, ICollection<IDataItem> dataItems)
        {
            this.CanvasSize = canvasSize;
            this.FitToX = fitToX;
            this.FitToY = fitToY;
            this.NewItems = dataItems;
        }

        public DrawOptions(DrawOptions options)
        {
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
            return this.CanvasSize.Equals(other.CanvasSize)
                && this.FitToX.Equals(other.FitToX)
                && this.FitToY.Equals(other.FitToY)
                && ((this.NewItems == null && other.NewItems == null) || this.NewItems != null ? this.NewItems.SequenceEqual(other.NewItems) : false);
        }

        public static bool operator ==(DrawOptions options1, DrawOptions options2)
        {
            return options1.Equals(options2);
        }

        public static bool operator !=(DrawOptions options1, DrawOptions options2)
        {
            return !options1.Equals(options2);
        }
    }
}
