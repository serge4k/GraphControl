using System;
using System.Drawing;
using System.Windows.Forms;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Models;
using GraphControl.Core.Structs;
using GraphControl.Core.Views;

namespace GraphControlWinForms
{
    internal class GraphControlView : PictureBox, IGraphControlView
    {
        #region Public properties
        /// <summary>
        /// Controls clients area size / ClientSize
        /// </summary>
        public global::GraphControl.Core.Structs.Size ControlSize => new global::GraphControl.Core.Structs.Size(this.ClientSize.Width, this.ClientSize.Height);

        /// <summary>
        /// Mouse action forwaring event overriding
        /// </summary>
        public new event EventHandler<ScaleUserSelectionEventArgs> MouseDown;

        /// <summary>
        /// Mouse action forwaring event overriding
        /// </summary>
        public new event EventHandler<ScaleUserSelectionEventArgs> MouseMove;

        /// <summary>
        /// Mouse action forwaring event overriding
        /// </summary>
        public new event EventHandler<ScaleUserSelectionEventArgs> MouseUp;

        /// <summary>
        /// Mouse action forwaring event overriding
        /// </summary>
        public new event EventHandler<ScaleUserSelectionEventArgs> MouseWheel;

        /// <summary>
        /// Sets view bounds/size
        /// </summary>
        /// <param name="left">position</param>
        /// <param name="top">position</param>
        /// <param name="width">size</param>
        /// <param name="height">size</param>
        public event EventHandler<DrawGraphEventArgs> DrawGraph;

        /// <summary>
        /// Control size was changed forwarding event (IControlViewSize)
        /// </summary>
        public event EventHandler<ControlSizeChangedEventArgs> ControlSizeChanged;
        #endregion

        #region Constructors
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
        #endregion

        #region Public methods
        /// <summary>
        /// Sets image in view from buffer
        /// </summary>
        /// <param name="bitmap">buffer with pre-rendered image</param>
        public void SetImage(Bitmap bitmap)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.SetImage(bitmap)));
            }
            else
            {
                this.Image = bitmap;
            }
        }

        /// <summary>
        /// Refresh view / control with default options (IRefreshControlView implementation)
        /// </summary>
        public void RefreshView()
        {
            IDrawOptions options = new DrawOptions(this.ControlSize, false, false, null);
            this.DrawGraph?.Invoke(this, new DrawGraphEventArgs(null, options));
        }

        /// <summary>
        /// Refresh view / control with options (IRefreshControlView implementation)
        /// </summary>
        public void RefreshView(IDrawOptions options)
        {
            this.DrawGraph?.Invoke(this, new DrawGraphEventArgs(null, options));
        }
        #endregion

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

        private static global::GraphControl.Core.Definitions.MouseButton ConvertMouseButton(System.Windows.Forms.MouseButtons button)
        {
            switch (button)
            {
                default:
                case System.Windows.Forms.MouseButtons.None:
                    return global::GraphControl.Core.Definitions.MouseButton.None;
                case System.Windows.Forms.MouseButtons.Left:
                    return global::GraphControl.Core.Definitions.MouseButton.Left;
                case System.Windows.Forms.MouseButtons.Right:
                    return global::GraphControl.Core.Definitions.MouseButton.Right;
                case System.Windows.Forms.MouseButtons.Middle:
                    return global::GraphControl.Core.Definitions.MouseButton.Middle;
                case System.Windows.Forms.MouseButtons.XButton1:
                    return global::GraphControl.Core.Definitions.MouseButton.XButton1;
                case System.Windows.Forms.MouseButtons.XButton2:
                    return global::GraphControl.Core.Definitions.MouseButton.XButton2;
            }
        }
        #endregion
    }
}
