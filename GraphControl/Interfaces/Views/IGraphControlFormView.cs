using System;
using GraphControlCore.Events;
using GraphControlCore.Interfaces.Services;
using GraphControlCore.Structs;

namespace GraphControlCore.Interfaces.Views
{
    public interface IGraphControlFormView : IView, IRegisterDataProvider
    {
        Size ControlSize { get; }

        event EventHandler FitToScreenByX;

        event EventHandler FitToScreenByY;

        event EventHandler<FitToScreenAlwaysEventArgs> FitToScreenAlways;

        event EventHandler<LoadEventArgs> Load;

        IItemFormatter ItemFormatter { get; set; }

        IMargin GraphMargin { get; }

        IBackgroundView UserBackgroundView { get; set; }

        IGridView UserGridView { get; set; }

        IDataView UserDataView { get; set; }

        IScalingSelectionView UserScalingSelectionView { get; set; }

        void Reset();

        void SetFitToScreenAlways(bool isChecked);

        void SetDivisionValues(double divX, double divY);

        void EnableFitByX(bool enabled);

        void EnableFitByY(bool enabled);
    }
}
