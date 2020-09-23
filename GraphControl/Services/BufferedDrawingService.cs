using System;
using System.Threading;
using System.Threading.Tasks;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Structs;
using GraphControl.Core.Utilities;

namespace GraphControl.Core.Services
{
    public class BufferedDrawingService : IBufferedDrawingService
    {
        #region Public properties
        public event EventHandler<UpdateScaleEventArgs> UpdateScale;

        public event EventHandler<DrawGraphEventArgs> DrawGraph;

        public event EventHandler<SetImageEventArgs> SetImage;

        public DateTime LastQueueOverflow { get; private set; }
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
            this.LastQueueOverflow = new DateTime(0);
        }
        #endregion

        #region Interface implementation
        public void DrawGraphInBufferAsync(DrawOptions options)
        {
            lock (this.drawingTaskSink)
            {
                this.drawingTaskCanvasOptions = options;

                if (this.drawingRequestEvent.WaitOne(0))
                {
                    this.LastQueueOverflow = DateTime.UtcNow;
                }

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

        protected virtual void DrawGraphInBuffer(DrawOptions drawOptions)
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Free();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        public void Free()
        {
            if (this.drawingBuffer != null)
            {
                this.drawingBuffer.Dispose();
            }

            this.drawingTask?.Dispose();

            this.drawingTaskCancellation.Dispose();

            this.drawingRequestEvent.Dispose();
        }
        #endregion
    }
}
