using System.Drawing;
using GraphControl.Core.Interfaces.Models;

namespace GraphControl.Core.Models
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
