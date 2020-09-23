using System;
using System.Drawing;
using GraphControl.Core.Events;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IGraphControlView : IControlView, IControlViewSize, IDrawingView
    {
        Structs.Size ControlSize { get; }

        event EventHandler<ScaleUserSelectionEventArgs> MouseDown;

        event EventHandler<ScaleUserSelectionEventArgs> MouseMove;

        event EventHandler<ScaleUserSelectionEventArgs> MouseUp;

        event EventHandler<ScaleUserSelectionEventArgs> MouseWheel;

        event EventHandler<DrawGraphEventArgs> DrawGraphInBuffer;

        void SetBounds(int left, int top, int width, int height);

        void SetImage(Bitmap bitmap);
    }
}
