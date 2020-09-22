using System.Collections.Generic;
using GraphControlCore.Definitions;
using GraphControlCore.Exceptions;
using GraphControlCore.Interfaces;
using GraphControlCore.Interfaces.Models;

namespace GraphControlCore.Utilities
{
    public class ItemFormatter : IItemFormatter
    {
        private readonly Dictionary<Axis, IValueFormatter> items;

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

        public string ToString(Axis axis, IDataItem item, double scaleStep)
        {
            if (item == null)
            {
                throw new GraphControlException("parameter \"item\" is null");
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
