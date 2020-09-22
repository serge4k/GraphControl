using System;
using System.Drawing;
using System.Windows.Forms;
using GraphControlCore.Events;
using GraphControlCore.Interfaces;
using GraphControlCore.Interfaces.Views;
using GraphControlCore.Structs;

namespace GraphControlWinForms
{
    internal class GraphControlView : PictureBox, IGraphControlView
    {
        public global::GraphControlCore.Structs.Size ControlSize { get => new global::GraphControlCore.Structs.Size(this.ClientSize.Width, this.ClientSize.Height); }

        public event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;

        public new event EventHandler<ScaleUserSelectionEventArgs> MouseDown;

        public new event EventHandler<ScaleUserSelectionEventArgs> MouseMove;

        public new event EventHandler<ScaleUserSelectionEventArgs> MouseUp;

        public new event EventHandler<ScaleUserSelectionEventArgs> MouseWheel;

        public event EventHandler<DrawGraphEventArgs> DrawGraphInBuffer;

        private DrawOptions drawOptions;

        public GraphControlView() : base()
        {
            // Whithout the ResizeRedraw statement, the control is redrawn only partially
            this.ResizeRedraw = true;

            // Autoresize control
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            base.MouseDown += CtrlView_MouseDown;
            base.MouseMove += CtrlView_MouseMove;
            base.MouseUp += CtrlView_MouseUp;
            base.MouseWheel += CtrlView_MouseWheel;
            base.SizeChanged += CtrlView_SizeChanged;
        }

        public void Draw(IDrawing drawing, DrawOptions options, IMargin margin)
        {
        }

        public void SetImage(Bitmap bitmap)
        {
            this.Image = bitmap;
        }

        public void SetDrawOptions(DrawOptions options)
        {
            this.drawOptions = new DrawOptions(options);
        }

        public void RefreshView()
        {
            this.DrawGraphInBuffer?.Invoke(this, new DrawGraphEventArgs(null, this.drawOptions));
        }

        #region Graph control handlers        
        private void CtrlView_SizeChanged(object sender, EventArgs e)
        {
            var size = this.ControlSize;
            this.ControlSizeChanged?.Invoke(this, new ControlSizeChangedEventArgs(size));
        }

        private void CtrlView_MouseDown(object sender, MouseEventArgs e)
        {
            ScaleUserSelectionEventArgs eventArgs = ToScaleUserSelectionEventArgs(e);
            this.MouseDown?.Invoke(this, eventArgs);
        }

        private void CtrlView_MouseMove(object sender, MouseEventArgs e)
        {
            ScaleUserSelectionEventArgs eventArgs = ToScaleUserSelectionEventArgs(e);
            this.MouseMove?.Invoke(this, eventArgs);
        }

        private void CtrlView_MouseUp(object sender, MouseEventArgs e)
        {
            ScaleUserSelectionEventArgs eventArgs = ToScaleUserSelectionEventArgs(e);
            this.MouseUp?.Invoke(this, eventArgs);
        }

        private void CtrlView_MouseWheel(object sender, MouseEventArgs e)
        {
            ScaleUserSelectionEventArgs eventArgs = ToScaleUserSelectionEventArgs(e);
            this.MouseWheel?.Invoke(this, eventArgs);
        }

        private static ScaleUserSelectionEventArgs ToScaleUserSelectionEventArgs(MouseEventArgs e)
        {
            return new ScaleUserSelectionEventArgs(
                ConvertMouseButton(e.Button),
                e.Location,
                e.Delta,
                Control.ModifierKeys == Keys.Shift
            );
        }

        private static global::GraphControlCore.Definitions.MouseButton ConvertMouseButton(System.Windows.Forms.MouseButtons button)
        {
            switch (button)
            {
                default:
                case System.Windows.Forms.MouseButtons.None:
                    return global::GraphControlCore.Definitions.MouseButton.None;
                case System.Windows.Forms.MouseButtons.Left:
                    return global::GraphControlCore.Definitions.MouseButton.Left;
                case System.Windows.Forms.MouseButtons.Right:
                    return global::GraphControlCore.Definitions.MouseButton.Right;
                case System.Windows.Forms.MouseButtons.Middle:
                    return global::GraphControlCore.Definitions.MouseButton.Middle;
                case System.Windows.Forms.MouseButtons.XButton1:
                    return global::GraphControlCore.Definitions.MouseButton.XButton1;
                case System.Windows.Forms.MouseButtons.XButton2:
                    return global::GraphControlCore.Definitions.MouseButton.XButton2;
            }
        }
        #endregion
    }
}
