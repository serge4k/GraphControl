﻿using System;
using System.Collections.Generic;
using GraphControl.Interfaces.Models;

namespace GraphControl.Events
{
    public class DataUpdatedEventArgs : EventArgs
    {
        public ICollection<IDataItem> Items { get; }

        public DataUpdatedEventArgs(IDataItem previousItem, IDataItem currentItem)
        {
            this.Items = new List<IDataItem>();
            this.Items.Add(previousItem);
            this.Items.Add(currentItem);
        }

        public DataUpdatedEventArgs(ICollection<IDataItem> items)
        {
            this.Items = items;
        }
    }
}
