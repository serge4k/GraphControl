using GraphControl.Events;

namespace GraphControl.Interfaces.Presenters
{
    public interface IScalingSelectionPresenter : IDrawingPresenter
    {
        void MouseDown(object sender, ScaleUserSelectionEventArgs e);

        void MouseMove(object sender, ScaleUserSelectionEventArgs e);

        void MouseUp(object sender, ScaleUserSelectionEventArgs e);

        void MouseWheel(object sender, ScaleUserSelectionEventArgs e);
    }
}