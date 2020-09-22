using System;
using System.Collections.Generic;
using GraphControlCore.Definitions;
using GraphControlCore.Events;
using GraphControlCore.Exceptions;
using GraphControlCore.Interfaces;
using GraphControlCore.Interfaces.Models;
using GraphControlCore.Interfaces.Services;
using GraphControlCore.Models;

namespace GraphControlCore.Services
{
    public class DataService : IDataService
    {
        #region Public properties
        public event EventHandler<DataUpdatedEventArgs> DataUpdated;

        public int ItemCount
        {
            get
            {
                lock (this.sink)
                {
                    return this.items.Count;
                }
            }            
        }
        #endregion

        #region Private fields
        private object sink = new object();
        private List<IDataItem> items = new List<IDataItem>();
        private DataItem minItem;
        private DataItem maxItem;
        private uint maxItems;
        #endregion

        #region Constructors
        public DataService() : this(10000000) // Limit for list size of two double X and Y = 16 bytes * maxItems
        {
        }

        public DataService(uint maxItems)
        {
            this.maxItems = maxItems;
        }
        #endregion

        #region Public methods
        public void RegisterDataProvider(IGraphDataProvider dataProvider)
        {
            if (dataProvider == null)
            {
                throw new GraphControlException("parameter is null");
            }
            dataProvider.OnReceiveData += DataProvider_OnReceiveData;

            // Subcribe to pre loaded data
            if (dataProvider is IGraphDataArrayProvider)
            {
                ((IGraphDataArrayProvider)dataProvider).OnGraphDataArrayReceived += DataService_OnGraphDataArrayReceived;
            }
        }

        public double GetMin(Axis axis)
        {
            lock (this.sink)
            {
                switch (axis)
                {
                    case Axis.X:
                        return (minItem != null) ? minItem.X : 0;
                    case Axis.Y:
                        return (minItem != null) ? minItem.Y : 0;
                    default:
                        throw new GraphControlException($"Axis index {axis.ToString()} is not supported");
                }
            }
        }

        public double GetMax(Axis axis)
        {
            lock (this.sink)
            {
                switch (axis)
                {
                    case Axis.X:
                        return (maxItem != null) ? maxItem.X : 0;
                    case Axis.Y:
                        return (maxItem != null) ? maxItem.Y : 0;
                    default:
                        throw new GraphControlException($"Axis index {axis.ToString()} is not supported");
                }
            }
        }
        
        public IEnumerable<IDataItem> GetItems(double startX, double endX)
        {
            lock (this.sink)
            {
                IDataItem prev = null;
                var enumerator = this.items.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.X >= endX)
                    {
                        break;
                    }
                    if (prev != null && enumerator.Current.X > startX)
                    {
                        yield return prev;
                    }
                    prev = enumerator.Current;
                }
                if (prev != null)
                {
                    yield return prev;
                }
                if (enumerator.Current != null)
                {
                    yield return enumerator.Current;
                }
            }            
        }
        #endregion

        #region Handlers
        private void DataProvider_OnReceiveData(double data, DateTime time)
        {
            var item = new Models.DataItem(time.Ticks / TimeSpan.TicksPerMillisecond, data);
            AddItem(item);
        }

        private void DataService_OnGraphDataArrayReceived(object sender, Events.GraphDataArrayEventArgs e)
        {
            AddItemRange(e.DataItems);
        }

        private void AddItem(DataItem item)
        {
            DataItem lastItem = null;
            lock (this.sink)
            {
                if (this.items.Count > this.maxItems)
                {
                    this.items.RemoveAt(0);
                }

                UpdateMinMax(item);
                if (this.items.Count > 0)
                {
                    lastItem = new DataItem(this.items[this.items.Count - 1]);
                }
                
                this.items.Add(item);
            }

            if (lastItem != null)
            {
                this.DataUpdated?.Invoke(this, new DataUpdatedEventArgs(lastItem, item));
            }
        }

        private void AddItemRange(ICollection<IDataItem> newItems)
        {
            lock (this.sink)
            {
                if (this.items.Count + newItems.Count > this.maxItems)
                {
                    this.items.RemoveRange(0, (int)(this.items.Count + newItems.Count  - this.maxItems));
                }

                foreach (var item in newItems)
                {
                    UpdateMinMax(item);
                }
                
                this.items.AddRange(newItems);
            }
            this.DataUpdated?.Invoke(this, new DataUpdatedEventArgs(newItems));
        }

        private void UpdateMinMax(IDataItem item)
        {
            if (this.minItem == null)
            {
                this.minItem = new DataItem(item);
            }
            else
            {
                if (this.minItem.X > item.X)
                {
                    this.minItem.X = item.X;
                }
            }

            if (this.maxItem == null)
            {
                this.maxItem = new DataItem(item);
            }
            else
            {
                if (this.maxItem.X < item.X)
                {
                    this.maxItem.X = item.X;
                }
            }

            if (this.minItem.Y > item.Y)
            {
                this.minItem.Y = item.Y;
            }

            if (this.maxItem.Y < item.Y)
            {
                this.maxItem.Y = item.Y;
            }
        }
        #endregion
    }
}
