using System;
using GraphControl.Core.Events;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IBufferedDrawingService : IDisposable
    {
        event EventHandler<UpdateScaleEventArgs> UpdateScale;

        event EventHandler<DrawGraphEventArgs> DrawGraph;

        event EventHandler<SetImageEventArgs> SetImage;

        DateTime LastQueueOverflow { get; }

        void DrawGraphInBufferAsync(DrawOptions options);
    }
}