namespace GraphControl.Core.Interfaces.Presenters
{
    public interface IGridPresenter : IDrawingPresenter
    {
        IMargin LabelMargin { get; set; }
    }
}
