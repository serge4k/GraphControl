using System.Drawing;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IBackgroundState
    {
        Color BackgroundColor { get; set; }

        Color PenColor { get; set; }
    }
}
