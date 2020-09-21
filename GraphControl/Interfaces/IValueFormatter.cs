namespace GraphControl.Interfaces
{
    public interface IValueFormatter
    {
        void AddFormat(double maxVal, string format);

        void AddStepFormat(double maxVal, string format, double divider);

        void AddDividers(double maxVal, double[] dividers);

        string ToString(double value, double step);

        string ToStepString(double step);

        double[] GetScaleDividers(double step);
    }
}