using System;
using GraphControl.Interfaces.Models;

namespace GraphControl.Models
{
    public class DataItem : IDataItem
    {
        public double X { get; set; }

        public double Y { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Used as coordinate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Used as coordinate")]
        public DataItem(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public DataItem(IDataItem dataItem)
        {
            this.X = dataItem.X;
            this.Y = dataItem.Y;
        }
    }
}
