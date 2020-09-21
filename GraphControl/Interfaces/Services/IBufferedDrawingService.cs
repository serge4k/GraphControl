using System;
using GraphControl.Events;
using GraphControl.Structs;

namespace GraphControl.Interfaces.Services
{
    public interface IBufferedDrawingService
    {
        event EventHandler<UpdateScaleEventArgs> UpdateScale;

        event EventHandler<DrawGraphEventArgs> DrawGraph;

        event EventHandler<SetImageEventArgs> SetImage;

        void DrawGraphInBufferAsync(DrawOptions options);
    }
}