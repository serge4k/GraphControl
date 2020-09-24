using System.Collections.Generic;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IDrawOptions
    {
        /// <summary>
        /// Canvas area width and heigth
        /// </summary>
        Size CanvasSize { get; }

        /// <summary>
        /// Fix by X option
        /// </summary>
        bool FitToX { get; }

        /// <summary>
        /// Fix by X option
        /// </summary>
        bool FitToY { get; }

        /// <summary>
        /// Contains new items or null when all items should be drawn
        /// </summary>
        ICollection<IDataItem> NewItems { get; }

        /// <summary>
        /// Returns true when draw only new data from NewItems
        /// </summary>
        bool DrawOnlyNewData { get; }
    }
}
