using System.Drawing;
using GraphControlCore.Interfaces.Models;

namespace GraphControlCore.Models
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
