using System.Collections.Generic;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Views
{
    public class GridDrawOptions : DrawOptions<IGridState>
    {
        public GridDrawOptions(IDrawOptions options, IGridState state) : base(options, state)
        {
        }

        public GridDrawOptions(Size canvasSize, bool fitToX, bool fitToY, ICollection<IDataItem> dataItems, IGridState state) : base(canvasSize, fitToX, fitToY, dataItems, state)
        {
        }
    }
}
