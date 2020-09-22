using GraphControlCore.Interfaces;
using GraphControlCore.Interfaces.Presenters;
using GraphControlCore.Interfaces.Views;
using GraphControlCore.Structs;

namespace GraphControlCore.Presenters
{
    public class GridPresenter : IGridPresenter, IDrawingPresenter
    {
        public IMargin LabelMargin
        {
            get
            {
                return this.view.LabelMargin;
            }
            set
            {
                this.view.LabelMargin = value;
            }
        }

        private readonly IGridView view;

        public GridPresenter(IGridView view)
        {
            this.view = view;
        }

        public void Draw(IDrawing drawing, DrawOptions options, IMargin margin)
        {
            this.view.Draw(drawing, options, margin);
        }
    }
}
