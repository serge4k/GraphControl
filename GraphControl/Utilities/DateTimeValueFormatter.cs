﻿using System;
namespace GraphControlCore.Utilities
{
    public class DateTimeValueFormatter : BaseValueFormatter
    {
        private readonly long ticksPerValue;

        public DateTimeValueFormatter() : this(string.Empty, TimeSpan.TicksPerMillisecond)
        {
        }

        public DateTimeValueFormatter(string format, long ticksPerValue) : base (format)
        {
            this.ticksPerValue = ticksPerValue;
        }

        public override string ToString(double value, double scaleStep)
        {
            var format = FindFormat(scaleStep);
            double ticks = value * ticksPerValue;
            if (ticks < 0)
            {
                ticks = 0;
            }
            if (ticks > DateTime.MaxValue.Ticks)
            {
                ticks = 0;
            }
            var dt = new DateTime((long)value * ticksPerValue);
            if (!String.IsNullOrWhiteSpace(format))
            {
                if (format.StartsWith("{0"))
                {
                    return String.Format(format, dt);
                }
                else
                {
                    return dt.ToString(format);
                }
                
            }
            else
            {
                return dt.ToString("yyyy.MM.dd HH:mm:ss");
            }
        }
    }
}
