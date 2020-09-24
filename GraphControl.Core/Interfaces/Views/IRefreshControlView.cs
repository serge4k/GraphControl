namespace GraphControl.Core.Interfaces.Views
{
    public interface IRefreshControlView
    {
        /// <summary>
        /// Refresh view / control with default options
        /// </summary>
        void RefreshView();

        /// <summary>
        /// Refresh view / control with options
        /// </summary>
        void RefreshView(IDrawOptions options);
    }
}
