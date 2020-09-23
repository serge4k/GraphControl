using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Models;

namespace GraphControl.Tests.Services
{
    internal class TestSinusDataProviderService : IGraphDataProvider, IGraphDataArrayProvider, IDataProviderService, IDisposable
    {
        public event GraphDataHandler OnReceiveData;

        public event EventHandler<GraphDataArrayEventArgs> OnGraphDataArrayReceived;

        public uint TestPoints { get; set; }

        private Timer timer;

        private long startTime;

        private long interval = 100;

        private long currentArg;

        public static IDataProviderService Create()
        {
            return TestSinusDataProviderService.Create(0);
        }

        public static IDataProviderService Create(int testPoints)
        {
            var provider = new TestSinusDataProviderService();
            provider.TestPoints = (uint)testPoints;
            return provider;
        }

        public TestSinusDataProviderService()
        {
            this.timer = new Timer();
            this.timer.Elapsed += Timer_Elapsed;
            this.timer.Interval = interval;
            this.timer.AutoReset = true;
        }

        public void Run()
        {
            this.Run((long)100);
        }

        public void Run(long interval)
        {
            this.interval = interval;
            this.startTime = this.currentArg = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            if (this.TestPoints > 0)
            {
                GenerateTestPoints(this.TestPoints);
            }

            this.timer.Enabled = true;
        }

        private void GenerateTestPoints(uint testPoints)
        {
            ICollection<IDataItem> dataArray = new List<IDataItem>((int)testPoints);
            for (int i = 0; i < testPoints; i++)
            {
                dataArray.Add(GenerateNextValue());
            }
            this.OnGraphDataArrayReceived?.Invoke(this, new GraphDataArrayEventArgs(dataArray));
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IDataItem dataItem = GenerateNextValue();
            OnReceiveData?.Invoke(dataItem.Y, new DateTime((long)dataItem.X * TimeSpan.TicksPerMillisecond));
        }

        private IDataItem GenerateNextValue()
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

        public void Dispose()
        {
            this.timer.Dispose();
        }
    }
}
