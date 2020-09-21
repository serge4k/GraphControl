using System;
using System.Collections.Generic;
using GraphControl.Interfaces.Models;

namespace GraphControl.Events
{
    public class DataUpdatedEventArgs : EventArgs
    {
        public ICollection<IDataItem> Items { get; set; }

        public DataUpdatedEventArgs(IDataItem prevItem, IDataItem item)
        {
            this.Items = new List<IDataItem>();
            this.Items.Add(prevItem);
            this.Items.Add(item);
        }

        public DataUpdatedEventArgs(ICollection<IDataItem> items)
        {
            this.Items = items;
        }
    }
}
