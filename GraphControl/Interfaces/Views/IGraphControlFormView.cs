using System;
using GraphControl.Events;
using GraphControl.Interfaces.Services;
using GraphControl.Structs;

namespace GraphControl.Interfaces.Views
{
    public interface IGraphControlFormView : IView, IControlViewSize, IRegisterDataProvider
    {
        Size ControlSize { get; }

        event EventHandler FitToScreenByX;

        event EventHandler FitToScreenByY;

        event EventHandler<FitToScreenAlwaysEventArgs> FitToScreenAlways;

        event EventHandler<LoadEventArgs> Load;

        void SetFitToScreenAlways(bool isChecked);

        void SetDivisionValues(double divX, double divY);

        void EnableFitByX(bool enabled);

        void EnableFitByY(bool enabled);
    }
}
