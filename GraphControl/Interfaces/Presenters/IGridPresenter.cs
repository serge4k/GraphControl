﻿namespace GraphControlCore.Interfaces.Presenters
{
    public interface IGridPresenter : IDrawingPresenter
    {
        IMargin LabelMargin { get; set; }
    }
}
