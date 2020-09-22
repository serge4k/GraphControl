using System.Collections.Generic;
using GraphControlCore.Definitions;
using GraphControlCore.Interfaces.Models;

namespace GraphControlCore.Interfaces.Services
{
    public interface IDataService : IRegisterDataProvider, IDataUpdated
    {
        double GetMin(Axis axis);

        double GetMax(Axis axis);

        IEnumerable<IDataItem> GetItems(double startX, double endX);

        int ItemCount { get; }
    }
}
