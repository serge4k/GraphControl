using System.Drawing;

namespace GraphControlCore.Interfaces.Models
{
    public interface IBackgroundState
    {
        Color BackgroundColor { get; set; }

        Color PenColor { get; set; }
    }
}
