using System;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Models;

namespace GraphControl.Tests.Services
{
    internal class TestSinusDataProviderService : TestBaseDataProvider
    {
        protected override IDataItem GenerateNextValue()
        {
            var x = this.currentArg;
            var y = Math.Sin(Math.PI * (this.currentArg - this.startTime) * 2 / (this.interval * this.interval));
            this.currentArg += this.interval;
            if (this.currentArg > DateTime.MaxValue.Ticks / TimeSpan.TicksPerMillisecond)
            {
                this.currentArg = 0;
            }
            return new DataItem(x, y);
        }
    }
}
