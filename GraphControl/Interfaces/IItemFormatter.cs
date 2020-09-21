using GraphControl.Definitions;
using GraphControl.Interfaces.Models;

namespace GraphControl.Interfaces
{
    public interface IItemFormatter
    {
        void Register(Axis axis, IValueFormatter presenter);

        string ToString(Axis axis, IDataItem item, double step);

        string ToStepString(Axis axis, double step);

        double[] GetScaleDivisions(Axis axis, double step);
    }
}