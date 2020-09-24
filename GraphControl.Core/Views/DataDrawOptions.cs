using System.Collections.Generic;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Views
{
    public class DataDrawOptions : DrawOptions<IDataDrawState>
    {
        public DataDrawOptions(IDrawOptions options, IDataDrawState state) : base(options, state)
        {
        }

        public DataDrawOptions(Size canvasSize, bool fitToX, bool fitToY, ICollection<IDataItem> dataItems, IDataDrawState state) : base(canvasSize, fitToX, fitToY, dataItems, state)
        {
        }
    }
}
