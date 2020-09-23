using System;
using System.Collections.Generic;
using System.Timers;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Models;

namespace GraphControl.Tests.Services
{
    internal abstract class TestBaseDataProvider : IDataProviderService, IGraphDataArrayProvider
    {
        public event GraphDataHandler OnReceiveData;

        public event EventHandler<GraphDataArrayEventArgs> OnGraphDataArrayReceived;

        public uint TestPoints { get; set; }

        private Timer timer;

        protected long startTime;

        protected long interval = 100;

        protected long currentArg;

        public static IDataProviderService Create(int testPoints)
        {
            var provider = new TestSinusDataProviderService();
            provider.TestPoints = (uint)testPoints;
            return provider;
        }

        public TestBaseDataProvider() : this(0)
        {
            this.timer = new Timer();
            this.timer.Elapsed += Timer_Elapsed;
            this.timer.Interval = interval;
            this.timer.AutoReset = true;
        }

        public TestBaseDataProvider(uint testPoints)
        {
            this.TestPoints = testPoints;
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

        protected abstract IDataItem GenerateNextValue();

        public virtual void Dispose()
        {
            this.timer.Dispose();
        }
    }
}
