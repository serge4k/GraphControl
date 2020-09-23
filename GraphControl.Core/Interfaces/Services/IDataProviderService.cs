using System;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IDataProviderService : IGraphDataProvider, IDisposable
    {
        uint TestPoints { get; set; }

        void Run();

        void Run(long interval);
    }
}