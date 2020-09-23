using System;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Structs;
using GraphControl.Core.Utilities;

namespace GraphControl.Tests.Views
{
    internal class TestGraphControlFormView : IGraphControlFormView
    {
        public Size ControlSize => new Size(640, 480);

        public IItemFormatter ItemFormatter { get; set; }

        public IMargin GraphMargin => new Margin(90, 5, 5, 40);

        public IMargin LabelMargin { get; set; }

        public IBackgroundView UserBackgroundView { get; set; }

        public IGridView UserGridView { get; set; }

        public IDataView UserDataView { get; set; }

        public IScalingSelectionView UserScalingSelectionView { get; set; }

#pragma warning disable CS0067
        public event EventHandler FitToScreenByX;

        public event EventHandler FitToScreenByY;

        public event EventHandler<FitToScreenAlwaysEventArgs> FitToScreenAlways;

        public event EventHandler<LoadEventArgs> Load;
#pragma warning restore CS0067

        public TestGraphControlFormView()
        {
            this.ItemFormatter = new ItemFormatter();
            this.LabelMargin = new Margin(5, 5, 5, 5);
        }

        public void EnableFitByX(bool enabled)
        {            
        }

        public void EnableFitByY(bool enabled)
        {
        }

        public void RegisterDataProvider(IGraphDataProvider dataProvider)
        {
        }

        public void Reset()
        {
        }

        public void SetDivisionValues(double divX, double divY)
        {
        }

        public void SetFitToScreenAlways(bool isChecked)
        {
        }

        public void Show()
        {
        }
    }
}
