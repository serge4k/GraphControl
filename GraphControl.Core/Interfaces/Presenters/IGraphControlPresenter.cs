using GraphControl.Core.Events;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Interfaces.Presenters
{
    public interface IGraphControlPresenter : IPresenter,
        IDataUpdated
    {
        void ControlSizeChanged(Size canvasSize);

        void UpdateFormState(IGraphControlFormState formState);

        void OnLoad(LoadEventArgs e);
    }
}
