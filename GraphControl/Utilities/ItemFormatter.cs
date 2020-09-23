using System.Collections.Generic;
using GraphControl.Core.Definitions;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;

namespace GraphControl.Core.Utilities
{
    public class ItemFormatter : IItemFormatter
    {
        private readonly Dictionary<Axis, IValueFormatter> items;

        public ItemFormatter()
        {
            this.items = new Dictionary<Axis, IValueFormatter>();
            Register(Axis.X, new DoubleValueFormatter());
            Register(Axis.Y, new DoubleValueFormatter());
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

        public string ToString(Axis axis, IDataItem item, double scaleStep)
        {
            if (item == null)
            {
                throw new InvalidArgumentException("parameter \"item\" is null");
            }
            if (this.items.ContainsKey(axis))
            {
                switch (axis)
                {
                    case Axis.X:
                        return this.items[axis].ToString(item.X, scaleStep);
                    case Axis.Y:
                        return this.items[axis].ToString(item.Y, scaleStep);
                }
            }
            throw new GraphControlException($"Axis index {axis.ToString()} is not found");
        }

        public string ToStepString(Axis axis, double scaleStep)
        {
            if (this.items.ContainsKey(axis))
            {
                switch (axis)
                {
                    case Axis.X:
                        return this.items[axis].ToStepString(scaleStep);
                    case Axis.Y:
                        return this.items[axis].ToStepString(scaleStep);
                }
            }
            throw new GraphControlException($"Axis index {axis.ToString()} is not found");
        }

        public double[] GetScaleDivisions(Axis axis, double scaleStep)
        {
            if (this.items.ContainsKey(axis))
            {
                switch (axis)
                {
                    case Axis.X:
                        return this.items[axis].GetScaleDividers(scaleStep);
                    case Axis.Y:
                        return this.items[axis].GetScaleDividers(scaleStep);
                }
            }
            throw new GraphControlException($"Axis index {axis.ToString()} is not found");
        }
    }
}
