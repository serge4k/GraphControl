using System.Collections.Generic;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Views
{
    public class BackgroundDrawOptions : DrawOptions<IBackgroundState>
    {
        public BackgroundDrawOptions(IDrawOptions options, IBackgroundState state)
            : base(options, state)
        {
        }

        public BackgroundDrawOptions(Size canvasSize, bool fitToX, bool fitToY, ICollection<IDataItem> dataItems, IBackgroundState state) 
            : base(canvasSize, fitToX, fitToY, dataItems, state)
        {
        }
    }
}