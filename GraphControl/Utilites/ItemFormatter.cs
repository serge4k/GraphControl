using System.Collections.Generic;
using GraphControl.Definitions;
using GraphControl.Exceptions;
using GraphControl.Interfaces;
using GraphControl.Interfaces.Models;

namespace GraphControl.Utilites
{
    public class ItemFormatter : IItemFormatter
    {
        private Dictionary<Axis, IValueFormatter> items;

        public ItemFormatter()
        {
            this.items = new Dictionary<Axis, IValueFormatter>();
        }

        public void Register(Axis axis, IValueFormatter presenter)
        {
            if (this.items.ContainsKey(axis))
            {
                this.items[axis] = presenter;
            }
            else
            {
                this.items.Add(axis, presenter);
            }
        }

        public string ToString(Axis axis, IDataItem item, double step)
        {
            if (this.items.ContainsKey(axis))
            {
                switch (axis)
                {
                    case Axis.X:
                        return this.items[axis].ToString(item.X, step);
                    case Axis.Y:
                        return this.items[axis].ToString(item.Y, step);
                }
            }
            throw new GraphControlException($"Axis index {axis.ToString()} is not found");
        }

        public string ToStepString(Axis axis, double step)
        {
            if (this.items.ContainsKey(axis))
            {
                switch (axis)
                {
                    case Axis.X:
                        return this.items[axis].ToStepString(step);
                    case Axis.Y:
                        return this.items[axis].ToStepString(step);
                }
            }
            throw new GraphControlException($"Axis index {axis.ToString()} is not found");
        }

        public double[] GetScaleDivisions(Axis axis, double step)
        {
            if (this.items.ContainsKey(axis))
            {
                switch (axis)
                {
                    case Axis.X:
                        return this.items[axis].GetScaleDividers(step);
                    case Axis.Y:
                        return this.items[axis].GetScaleDividers(step);
                }
            }
            throw new GraphControlException($"Axis index {axis.ToString()} is not found");
        }
    }
}
