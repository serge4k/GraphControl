using System.Windows.Forms;

namespace GraphControlWinFormsTestApp
{
    internal class GraphCanvas : PictureBox
    {
        public GraphCanvas() : base()
        {
            // Whithout the ResizeRedraw statement, the control is redrawn only partially
            this.ResizeRedraw = true;
        }
    }
}
