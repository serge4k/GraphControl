using GraphControlCore.Definitions;
using GraphControlCore.Interfaces.Models;

namespace GraphControlCore.Interfaces
{
    public interface IItemFormatter
    {
        void Register(Axis axis, IValueFormatter presenter);

        string ToString(Axis axis, IDataItem item, double scaleStep);

        string ToStepString(Axis axis, double scaleStep);

        double[] GetScaleDivisions(Axis axis, double scaleStep);
    }
}