namespace GraphControl.Interfaces.Views
{
    public interface IGridView : IDrawingView
    {
        IMargin LabelMargin { get; set; }
    }
}