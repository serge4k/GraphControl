using GraphControl.Core.Structs;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IControlView : IView
    {
        void RefreshView();

        void RefreshView(DrawOptions options);
    }
}
