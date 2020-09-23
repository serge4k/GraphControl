using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Presenters;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Presenters
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
