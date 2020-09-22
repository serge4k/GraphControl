using GraphControl.Definitions;
using GraphControl.Interfaces.Models;

namespace GraphControl.Interfaces
{
    public interface IItemFormatter
    {
        void Register(Axis axis, IValueFormatter presenter);

        string ToString(Axis axis, IDataItem item, double scaleStep);

        string ToStepString(Axis axis, double scaleStep);

        double[] GetScaleDivisions(Axis axis, double scaleStep);
    }
}