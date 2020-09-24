using GraphControl.Core.Events;
using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Interfaces.Presenters
{
    public interface IGraphControlPresenter : IPresenter
    {
        /// <summary>
        /// Notifies child control about parent control state changed
        /// </summary>
        /// <param name="formState"></param>
        void UpdateFormState(IGraphControlFormState formState);

        /// <summary>
        /// Notifies child control about parent control is loaded and control size should be updated
        /// </summary>
        /// <param name="e">contains client rect</param>
        void OnLoad(LoadEventArgs e);
    }
}
