using System;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IBufferedDrawingService : IDrawGraph, IDisposable
    {
        /// <summary>
        /// Callback to update scale during draw
        /// </summary>
        event EventHandler<UpdateScaleEventArgs> UpdateScale;

        /// <summary>
        /// Callback to set image
        /// </summary>
        event EventHandler<SetImageEventArgs> SetImage;

        /// <summary>
        /// Queue overflow datetime (for unit tests)
        /// </summary>
        DateTime LastQueueOverflow { get; }

        /// <summary>
        /// Posts event to draw in buffer async
        /// </summary>
        /// <param name="options">draw options</param>
        void DrawGraphInBufferAsync(IDrawOptions options);
    }
}