using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using GraphControl.Events;
using GraphControl.Interfaces;
using GraphControl.Interfaces.Models;
using GraphControl.Interfaces.Services;
using GraphControl.Models;
using static GraphControl.Events.Delegates;

namespace GraphControlWinFormsTestApp.Services
{
    internal class SinusDataProviderService : IGraphDataProvider, IGraphDataArrayProvider, IDataProviderService
    {
        public event GraphDataHandler OnReceiveData;

        public event EventHandler<GraphDataArrayEventArgs> OnGraphDataArrayReceived;

        public uint TestPoints { get; set; }

        private Timer timer;

        private long startTime;

        private long interval = 100;

        private long currentArg;
            
        public SinusDataProviderService()
        {
        }

        public void Run()
        {
            Run(100);
        }

        public void Run(long interval)
        {
            this.interval = interval;
            this.startTime = this.currentArg = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            if (this.TestPoints > 0)
            {
                GenerateTestPoints(this.TestPoints);
            }
                        
            this.timer = new Timer();
            this.timer.Elapsed += Timer_Elapsed;
            this.timer.Interval = interval;
            this.timer.AutoReset = true;
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
    }
}
