using System.Collections.Generic;
using GraphControl.Definitions;
using GraphControl.Interfaces.Models;

namespace GraphControl.Interfaces.Services
{
    public interface IDataService : IRegisterDataProvider, IDataUpdated
    {
        double GetMin(Axis axis);

        double GetMax(Axis axis);

        IEnumerable<IDataItem> GetItems(double startX, double endX);

        int ItemCount { get; }
    }
}
