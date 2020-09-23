using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces.Models;

namespace GraphControl.Core.Models
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
            if (dataItem == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            this.X = dataItem.X;
            this.Y = dataItem.Y;
        }
    }
}
