using GraphControl.Events;
using GraphControl.Interfaces.Models;
using GraphControl.Interfaces.Services;
using GraphControl.Structs;

namespace GraphControl.Interfaces.Presenters
{
    public interface IGraphControlPresenter : IPresenter,
        IDataUpdated, IScaleUpdate
    {
        void ControlSizeChanged(Size canvasSize);

        void UpdateFormState(IGraphControlFormState state);

        void OnLoad(LoadEventArgs e);
    }
}
