using GraphControlCore.Events;
using GraphControlCore.Interfaces.Models;
using GraphControlCore.Interfaces.Services;
using GraphControlCore.Structs;

namespace GraphControlCore.Interfaces.Presenters
{
    public interface IGraphControlPresenter : IPresenter,
        IDataUpdated, IScaleUpdate
    {
        void ControlSizeChanged(Size canvasSize);

        void UpdateFormState(IGraphControlFormState formState);

        void OnLoad(LoadEventArgs e);
    }
}
