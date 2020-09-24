using System.Drawing;
using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Views
{
    public class DataDrawState : IDataDrawState
    {
        public Color LineColor { get; set; }

        public DataDrawState()
        {
            this.LineColor = Color.Red;
        }
    }
}
