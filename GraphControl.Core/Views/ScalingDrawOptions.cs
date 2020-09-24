using System.Collections.Generic;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Views
{
    public class ScalingDrawOptions : DrawOptions<IScalingState>
    {
        public ScalingDrawOptions(IDrawOptions options, IScalingState state) : base(options, state)
        {
        }

        public ScalingDrawOptions(Size canvasSize, bool fitToX, bool fitToY, ICollection<IDataItem> dataItems, IScalingState state) : base(canvasSize, fitToX, fitToY, dataItems, state)
        {
        }
    }
}
