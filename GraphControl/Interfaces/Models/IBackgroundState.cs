using System.Drawing;

namespace GraphControl.Interfaces.Models
{
    public interface IBackgroundState
    {
        Color BackgroundColor { get; set; }

        Color PenColor { get; set; }
    }
}
