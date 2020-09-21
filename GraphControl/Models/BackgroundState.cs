using System.Drawing;
using GraphControl.Interfaces.Models;

namespace GraphControl.Models
{
    public class BackgroundState : IBackgroundState
    {
        public Color BackgroundColor { get; set; }

        public Color PenColor { get; set; }

        public BackgroundState()
        {
            this.BackgroundColor = Color.White;
            this.PenColor = Color.Black;
        }
    }
}
