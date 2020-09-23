using System;
using System.Collections.Generic;
using GraphControl.Core.Interfaces.Models;

namespace GraphControl.Core.Events
{
    public class GraphDataArrayEventArgs : EventArgs
    {
        public ICollection<IDataItem> DataItems { get; private set; }

        public GraphDataArrayEventArgs(ICollection<IDataItem> dataItems)
        {
            this.DataItems = dataItems;
        }
    }
}
