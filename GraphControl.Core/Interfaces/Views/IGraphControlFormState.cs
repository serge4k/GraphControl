namespace GraphControl.Core.Interfaces.Views
{
    public interface IGraphControlFormState
    {
        /// <summary>
        /// Fit to screen by X once
        /// </summary>
        bool FitToScreenByX { get; set; }

        /// <summary>
        /// Fit to screen by Y once
        /// </summary>
        bool FitToScreenByY { get; set; }

        /// <summary>
        /// Scale by X and Y and update when new value was received
        /// </summary>
        bool FitToScreenAlways { get; set; }
    }
}
