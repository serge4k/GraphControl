using System.Collections.Generic;
using GraphControl.Core.Definitions;
using GraphControl.Core.Interfaces.Models;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IDataService : IRegisterDataProvider, IDataUpdated
    {
        double GetMin(Axis axis);

        double GetMax(Axis axis);

        IEnumerable<IDataItem> GetItems(double startX, double endX);

        int ItemCount { get; }
    }
}
