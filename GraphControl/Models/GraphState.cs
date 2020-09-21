using System.Drawing;
using GraphControl.Interfaces.Models;

namespace GraphControl.Models
{
    public class GraphState : IGraphState
    {
        public Color LineColor { get; set; }

        public GraphState()
        {
            this.LineColor = Color.Red;
        }
    }
}
