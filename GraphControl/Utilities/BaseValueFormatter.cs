using System;
using System.Collections.Generic;
using GraphControlCore.Interfaces;

namespace GraphControlCore.Utilities
{
    public class BaseValueFormatter : IValueFormatter
    {
        private readonly SortedDictionary<double, string> formats;

        private readonly SortedDictionary<double, string> stepFormats;

        private readonly SortedDictionary<double, double> stepDividers;

        private readonly SortedDictionary<double, double[]> dividers;

        private readonly double[] defaultDividers = { 2, 5, 10 };

        public BaseValueFormatter() : this(string.Empty)
        {
        }

        public BaseValueFormatter(string format)
        {
            this.formats = new SortedDictionary<double, string>();
            if (!String.IsNullOrEmpty(format))
            {
                AddFormat(Double.MaxValue, format);
            }

            this.stepFormats = new SortedDictionary<double, string>();

            this.stepDividers = new SortedDictionary<double, double>();

            this.dividers = new SortedDictionary<double, double[]>();
        }

        public void AddFormat(double maxValue, string format)
        {
            this.formats.Add(maxValue, format);
        }

        public void AddStepFormat(double maxValue, string format, double divider)
        {
            this.stepFormats.Add(maxValue, format);
            this.stepDividers.Add(maxValue, divider);
        }

        public void AddDividers(double maxValue, double[] newDividers)
        {
            this.dividers.Add(maxValue, newDividers);
        }

        protected string FindFormat(double value)
        {
            foreach (var pair in this.formats)
            {
                if (value < pair.Key)
                {
                    return pair.Value;
                }
            }
            return string.Empty;
        }

        protected string FindStepFormat(double value)
        {
            foreach (var pair in this.stepFormats)
            {
                if (value < pair.Key)
                {
                    return pair.Value;
                }
            }
            return string.Empty;
        }

        protected double FindStepDivider(double value)
        {
            foreach (var pair in this.stepDividers)
            {
                if (value < pair.Key)
                {
                    return pair.Value;
                }
            }
            return 1;
        }

        protected double[] FindDivider(double scaleStep)
        {
            foreach (var pair in this.dividers)
            {
                if (scaleStep < pair.Key)
                {
                    return pair.Value;
                }
            }
            return this.defaultDividers;
        }

        public virtual double[] GetScaleDividers(double scaleStep)
        {
            return this.FindDivider(scaleStep);
        }

        public virtual string ToString(double value, double scaleStep)
        {
            var format = FindFormat(scaleStep);
            if (!String.IsNullOrWhiteSpace(format))
            {
                if (format.StartsWith("{0"))
                {
                    return String.Format(format, value);
                }
                else
                {
                    return value.ToString(format);
                }
            }
            else
            {
                return value.ToString();
            }
        }

        public virtual string ToStepString(double scaleStep)
        {
            var format = FindStepFormat(scaleStep);
            var divider = FindStepDivider(scaleStep);
            if (!String.IsNullOrWhiteSpace(format))
            {
                if (format.StartsWith("{0"))
                {
                    return String.Format(format, scaleStep / divider);
                }
                else
                {
                    return (scaleStep / divider).ToString(format);
                }                
            }
            else
            {
                return (scaleStep / divider).ToString();
            }
        }
    }
}
