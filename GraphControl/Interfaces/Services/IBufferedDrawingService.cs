using System;
using GraphControlCore.Events;
using GraphControlCore.Structs;

namespace GraphControlCore.Interfaces.Services
{
    public interface IBufferedDrawingService
    {
        event EventHandler<UpdateScaleEventArgs> UpdateScale;

        event EventHandler<DrawGraphEventArgs> DrawGraph;

        event EventHandler<SetImageEventArgs> SetImage;

        void DrawGraphInBufferAsync(DrawOptions options);
    }
}