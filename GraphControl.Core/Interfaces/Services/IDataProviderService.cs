using System;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IDataProviderService : IGraphDataProvider, IDisposable
    {
        /// <summary>
        /// Pre-generate points
        /// </summary>
        uint TestPoints { get; set; }

        /// <summary>
        /// Starts the provider
        /// </summary>
        void Run();

        /// <summary>
        /// Start the provider to generate data with inverval in milliseconds
        /// </summary>
        /// <param name="interval">inverval in milliseconds</param>
        void Run(long interval);
    }
}