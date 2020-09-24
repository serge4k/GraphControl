using System.Drawing;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IDataDrawState
    {
        /// <summary>
        /// Color for lines and points
        /// </summary>
        Color LineColor { get; set; }
    }
}
