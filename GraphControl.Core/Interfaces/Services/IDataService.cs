using System.Collections.Generic;
using GraphControl.Core.Definitions;
using GraphControl.Core.Interfaces.Models;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IDataService : IRegisterDataProvider, IDataUpdated
    {
        /// <summary>
        /// Data items number
        /// </summary>
        int ItemCount { get; }

        /// <summary>
        /// Returns cached min value for stored data for X or Y
        /// </summary>
        /// <param name="axis">X or Y</param>
        /// <returns>min value</returns>
        double GetMin(Axis axis);

        /// <summary>
        /// Returns cached max value for stored data for X or Y
        /// </summary>
        /// <param name="axis">X or Y</param>
        /// <returns>max value</returns>
        double GetMax(Axis axis);

        /// <summary>
        /// Enumerates items
        /// </summary>
        /// <param name="startX">filter by min startX value</param>
        /// <param name="endX">filter by max endX value</param>
        /// <returns></returns>
        IEnumerable<IDataItem> GetItems(double startX, double endX);
    }
}
