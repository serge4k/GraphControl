using System;
using System.Threading;
using System.Threading.Tasks;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Utilities;
using GraphControl.Core.Exceptions;
using System.Collections.Generic;

namespace GraphControl.Core.Services
{
    public class BufferedDrawingService : IBufferedDrawingService
    {
        #region Public properties
        /// <summary>
        /// Callback to update scale during draw
        /// </summary>
        public event EventHandler<UpdateScaleEventArgs> UpdateScale;

        /// <summary>
        /// Callback to set image
        /// </summary>
        public event EventHandler<DrawGraphEventArgs> DrawGraph;

        /// <summary>
        /// Queue overflow datetime (for unit tests)
        /// </summary>
        public event EventHandler<SetImageEventArgs> SetImage;

        /// <summary>
        /// Posts event to draw in buffer async
        /// </summary>
        /// <param name="options">draw options</param>
        public DateTime LastQueueOverflow { get; private set; }
        #endregion

        #region Private fields
        private DrawingBuffer drawingBuffer;
        private Task drawingTask;
        private ManualResetEvent drawingRequestEvent;
        private CancellationTokenSource drawingTaskCancellation;
        private object drawingTaskSink;
        private Queue<IDrawOptions> drawingTaskCanvasOptions;
        #endregion

        #region Constructors
        public BufferedDrawingService()
        {
            this.drawingRequestEvent = new ManualResetEvent(false);
            this.drawingTaskCancellation = new CancellationTokenSource();
            this.drawingTaskSink = new object();
            this.LastQueueOverflow = new DateTime(0);
            this.drawingTaskCanvasOptions = new Queue<IDrawOptions>(5);
        }
        #endregion

        #region Interface implementation
        /// <summary>
        /// Posts event to draw in buffer async
        /// </summary>
        /// <param name="options">draw options</param>
        public void DrawGraphInBufferAsync(IDrawOptions options)
        {
            lock (this.drawingTaskSink)
            {
                if (this.drawingTaskCanvasOptions.Count > 2)
                {
                    this.LastQueueOverflow = DateTime.UtcNow;
                    this.drawingTaskCanvasOptions.Dequeue();
                }

                this.drawingTaskCanvasOptions.Enqueue(options);

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
                        IDrawOptions options;
                        bool empty = false;
                        while (!empty)
                        {
                            options = null;
                            lock (this.drawingTaskSink)
                            {
                                this.drawingRequestEvent.Reset();
                                empty = drawingTaskCanvasOptions.Count == 0;
                                if (!empty)
                                {
                                    options = this.drawingTaskCanvasOptions.Dequeue();
                                }                                
                            }
                            if (options != null)
                            {
                                DrawGraphInBuffer(options);
                                if (this.drawingBuffer != null)
                                {
                                    this.SetImage?.Invoke(this, new SetImageEventArgs(this.drawingBuffer.Bitmap));
                                }
                            }
                        }                                                
                    }
                }
                finally
                {

                }
            }
        }

        protected virtual void DrawGraphInBuffer(IDrawOptions options)
        {
            if (options == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }

            // Do not draw if canvas size is 0
            if (options.CanvasSize.Width == 0 || options.CanvasSize.Height == 0)
            {
                return;
            }

            // Check that buffer was not created or recreate when size was changed
            if (this.drawingBuffer == null ||
                this.drawingBuffer.CanvasSize.Width != options.CanvasSize.Width ||
                this.drawingBuffer.CanvasSize.Height != options.CanvasSize.Height)
            {
                this.drawingBuffer = new DrawingBuffer(options.CanvasSize);
            }

            this.UpdateScale?.Invoke(this, new UpdateScaleEventArgs(options));

            // Draw in buffer
            using (var drawing = new Drawing2DWrapper(this.drawingBuffer.Graphics))
            {
                this.DrawGraph?.Invoke(this, new DrawGraphEventArgs(drawing, options));
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
            // uncomment the following line if the finalizer is overridden above.
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
