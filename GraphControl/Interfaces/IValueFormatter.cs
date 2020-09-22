namespace GraphControl.Interfaces
{
    public interface IValueFormatter
    {
        void AddFormat(double maxValue, string format);

        void AddStepFormat(double maxValue, string format, double divider);

        void AddDividers(double maxValue, double[] newDividers);

        string ToString(double value, double scaleStep);

        string ToStepString(double scaleStep);

        double[] GetScaleDividers(double scaleStep);
    }
}