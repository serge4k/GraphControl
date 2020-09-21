using System;
using System.Threading;
using System.Threading.Tasks;
using GraphControl.Events;
using GraphControl.Interfaces.Services;
using GraphControl.Structs;
using GraphControl.Utilites;

namespace GraphControl.Services
{
    public sealed class BufferedDrawingService : IDisposable, IBufferedDrawingService
    {
        #region Public events
        public event EventHandler<UpdateScaleEventArgs> UpdateScale;

        public event EventHandler<DrawGraphEventArgs> DrawGraph;

        public event EventHandler<SetImageEventArgs> SetImage;
        #endregion

        #region Private fields
        private DrawingBuffer drawingBuffer;
        private Task drawingTask;
        private ManualResetEvent drawingRequestEvent;
        private CancellationTokenSource drawingTaskCancellation;
        private object drawingTaskSink;
        private DrawOptions drawingTaskCanvasOptions;
        #endregion

        #region Constructors
        public BufferedDrawingService()
        {
            this.drawingRequestEvent = new ManualResetEvent(false);
            this.drawingTaskCancellation = new CancellationTokenSource();
            this.drawingTaskSink = new object();
        }
        #endregion

        #region Interface implementation
        public void DrawGraphInBufferAsync(DrawOptions options)
        {
            lock (this.drawingTaskSink)
            {
                this.drawingTaskCanvasOptions = options;

                this.drawingRequestEvent.Set();

                if (this.drawingTask == null)
                {
                    this.drawingTask = Task.Factory.StartNew(new Action(DrawGraphInBufferAction));
                }
            }
        }
        #endregion

        #region Private methods
        private void DrawGraphInBufferAction()
        {
            while (!this.drawingTaskCancellation.IsCancellationRequested)
            {
                try
                {
                    if (EventWaitHandle.WaitAny(new[] { this.drawingTaskCancellation.Token.WaitHandle, this.drawingRequestEvent }) == 1)
                    {
                        DrawOptions options;
                        lock (this.drawingTaskSink)
                        {
                            options = this.drawingTaskCanvasOptions;
                        }
                        this.drawingRequestEvent.Reset();
                        DrawGraphInBuffer(options);
                        if (this.drawingBuffer != null)
                        {
                            this.SetImage?.Invoke(this, new SetImageEventArgs(this.drawingBuffer.Bitmap));
                        }                        
                    }
                }
                finally
                {

                }
            }
        }

        private void DrawGraphInBuffer(DrawOptions drawOptions)
        {
            // Do not draw if canvas size is 0
            if (drawOptions.CanvasSize.Width == 0 || drawOptions.CanvasSize.Height == 0)
            {
                return;
            }

            // Check that buffer was not created or recreate when size was changed
            if (this.drawingBuffer == null ||
                this.drawingBuffer.CanvasSize.Width != drawOptions.CanvasSize.Width ||
                this.drawingBuffer.CanvasSize.Height != drawOptions.CanvasSize.Height)
            {
                this.drawingBuffer = new DrawingBuffer(drawOptions.CanvasSize);
            }

            this.UpdateScale?.Invoke(this, new UpdateScaleEventArgs(drawOptions));

            // Draw in buffer
            using (var drawing = new Drawing2DWrapper(this.drawingBuffer.Graphics))
            {
                this.DrawGraph?.Invoke(this, new DrawGraphEventArgs(drawing, drawOptions));
            }
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            this.drawingBuffer?.Dispose();
            this.drawingTask?.Dispose();
        }
        #endregion
    }
}
