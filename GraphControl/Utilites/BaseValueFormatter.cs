using System;
using System.Collections.Generic;
using GraphControl.Interfaces;

namespace GraphControl.Utilites
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

        public void AddFormat(double maxVal, string format)
        {
            this.formats.Add(maxVal, format);
        }

        public void AddStepFormat(double maxVal, string format, double divider)
        {
            this.stepFormats.Add(maxVal, format);
            this.stepDividers.Add(maxVal, divider);
        }

        public void AddDividers(double maxVal, double[] dividers)
        {
            this.dividers.Add(maxVal, dividers);
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

        protected double[] FindDivider(double step)
        {
            foreach (var pair in this.dividers)
            {
                if (step < pair.Key)
                {
                    return pair.Value;
                }
            }
            return this.defaultDividers;
        }

        public virtual double[] GetScaleDividers(double step)
        {
            return this.FindDivider(step);
        }

        public virtual string ToString(double value, double step)
        {
            var format = FindFormat(step);
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

        public virtual string ToStepString(double step)
        {
            var format = FindStepFormat(step);
            var divider = FindStepDivider(step);
            if (!String.IsNullOrWhiteSpace(format))
            {
                if (format.StartsWith("{0"))
                {
                    return String.Format(format, step / divider);
                }
                else
                {
                    return (step / divider).ToString(format);
                }                
            }
            else
            {
                return (step / divider).ToString();
            }
        }
    }
}
