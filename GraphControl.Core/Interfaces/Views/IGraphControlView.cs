using System;
using System.Drawing;
using GraphControl.Core.Events;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IGraphControlView : IView, IRefreshControlView, IControlViewSize, IDrawGraph
    {
        /// <summary>
        /// Controls clients area size / ClientSize
        /// </summary>
        Structs.Size ControlSize { get; }

        /// <summary>
        /// Mouse action forwaring event
        /// </summary>
        event EventHandler<ScaleUserSelectionEventArgs> MouseDown;

        /// <summary>
        /// Mouse action forwaring event
        /// </summary>
        event EventHandler<ScaleUserSelectionEventArgs> MouseMove;

        /// <summary>
        /// Mouse action forwaring event
        /// </summary>
        event EventHandler<ScaleUserSelectionEventArgs> MouseUp;

        /// <summary>
        /// Mouse action forwaring event
        /// </summary>
        event EventHandler<ScaleUserSelectionEventArgs> MouseWheel;

        /// <summary>
        /// Sets view bounds/size
        /// </summary>
        /// <param name="left">position</param>
        /// <param name="top">position</param>
        /// <param name="width">size</param>
        /// <param name="height">size</param>
        void SetBounds(int left, int top, int width, int height);

        /// <summary>
        /// Sets image in view from buffer
        /// </summary>
        /// <param name="bitmap">buffer with pre-rendered image</param>
        void SetImage(Bitmap bitmap);
    }
}
