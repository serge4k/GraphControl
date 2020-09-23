using System.Drawing;

namespace GraphControl.Core.Interfaces.Models
{
    public interface IBackgroundState
    {
        Color BackgroundColor { get; set; }

        Color PenColor { get; set; }
    }
}
