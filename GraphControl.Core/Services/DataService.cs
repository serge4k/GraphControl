using System;
using System.Collections.Generic;
using System.Linq;
using GraphControl.Core.Definitions;
using GraphControl.Core.Events;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Models;

namespace GraphControl.Core.Services
{
    public class DataService : IDataService
    {
        #region Public properties
        /// <summary>
        /// Data updated core event to notify about new DataItems block was received
        /// </summary>
        public event EventHandler<DataUpdatedEventArgs> DataUpdated;

        /// <summary>
        /// Data items number
        /// </summary>
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
            if (maxItems == 0)
            {
                throw new InvalidArgumentException("parameter max item is 0");
            }
            this.maxItems = maxItems;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Registers external data provider (IRegisterDataProvider)
        /// </summary>
        /// <param name="dataProvider">data provider</param>
        public void RegisterDataProvider(IGraphDataProvider dataProvider)
        {
            if (dataProvider == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            dataProvider.OnReceiveData += DataProvider_OnReceiveData;

            // Subcribe to pre loaded data
            if (dataProvider is IGraphDataArrayProvider)
            {
                ((IGraphDataArrayProvider)dataProvider).OnGraphDataArrayReceived += DataService_OnGraphDataArrayReceived;
            }
        }

        /// <summary>
        /// Returns cached min value for stored data for X or Y
        /// </summary>
        /// <param name="axis">X or Y</param>
        /// <returns>min value</returns>
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

        /// <summary>
        /// Returns cached max value for stored data for X or Y
        /// </summary>
        /// <param name="axis">X or Y</param>
        /// <returns>max value</returns>
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

        /// <summary>
        /// Enumerates items
        /// </summary>
        /// <param name="startX">filter by min startX value</param>
        /// <param name="endX">filter by max endX value</param>
        /// <returns></returns>
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
            ICollection<IDataItem> addedItems = null;
            lock (this.sink)
            {
                if (this.items.Count + newItems.Count > this.maxItems)
                {
                    // the collection will be overflowing
                    int itemsToRemove = (int)(this.items.Count + newItems.Count - this.maxItems);
                    if (itemsToRemove <= this.items.Count)
                    {
                        // remove part and add new items
                        this.items.RemoveRange(0, itemsToRemove);
                        addedItems = newItems;                 
                    }
                    else
                    {
                        // remove all items
                        this.items.Clear();
                        if (newItems.Count > this.maxItems)
                        {
                            // add last maxItems from newItems
                            addedItems = newItems.ToList()
                                .GetRange((int)(newItems.Count - this.maxItems - 1), (int)this.maxItems);
                        }
                        else
                        {
                            // add all newItems
                            addedItems = newItems;
                        }
                    }
                }
                else
                {
                    addedItems = newItems;
                }
                this.items.AddRange(addedItems);
                foreach (var item in addedItems)
                {
                    UpdateMinMax(item);
                }
            }
            this.DataUpdated?.Invoke(this, new DataUpdatedEventArgs(addedItems));
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
