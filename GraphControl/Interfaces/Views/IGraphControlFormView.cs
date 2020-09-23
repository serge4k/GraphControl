using System;
using GraphControl.Core.Events;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Interfaces.Views
{
    public interface IGraphControlFormView : IView, IRegisterDataProvider
    {
        /// <summary>
        /// Size of control's client area
        /// </summary>
        Size ControlSize { get; }

        /// <summary>
        /// MVP design view handler
        /// </summary>
        event EventHandler FitToScreenByX;

        /// <summary>
        /// MVP design view handler
        /// </summary>
        event EventHandler FitToScreenByY;

        /// <summary>
        /// MVP design view handler
        /// </summary>
        event EventHandler<FitToScreenAlwaysEventArgs> FitToScreenAlways;

        /// <summary>
        /// MVP design view handler
        /// </summary>
        event EventHandler<LoadEventArgs> Load;

        /// <summary>
        /// Defines ItemFormatter, use the Reset() method to re-create Control with new formatter
        /// </summary>
        IItemFormatter ItemFormatter { get; set; }

        /// <summary>
        /// Defines GraphMargin, use the Reset() method to re-create Control with new margin
        /// </summary>
        IMargin GraphMargin { get; }

        /// <summary>
        /// Defines LabelMargin, use the Reset() method to re-create Control with new margin
        /// </summary>
        IMargin LabelMargin { get; set; }

        /// <summary>
        /// Background custom view
        /// </summary>
        IBackgroundView UserBackgroundView { get; set; }

        /// <summary>
        /// Grid custom view
        /// </summary>
        IGridView UserGridView { get; set; }

        /// <summary>
        /// Data custom view
        /// </summary>
        IDataView UserDataView { get; set; }

        /// <summary>
        /// Scaling custom view
        /// </summary>
        IScalingSelectionView UserScalingSelectionView { get; set; }

        /// <summary>
        /// Reset internal objects with new properties
        /// </summary>
        void Reset();

        /// <summary>
        /// MVP design View interface
        /// </summary>
        /// <param name="isChecked">true to enable automatical scaling</param>
        void SetFitToScreenAlways(bool isChecked);

        /// <summary>
        /// MVP design View interface
        /// </summary>
        /// <param name="enabled">true to enable Fit By X button</param>
        void EnableFitByX(bool enabled);

        /// <summary>
        /// MVP design View interface
        /// </summary>
        /// <param name="enabled">true to enable Fit By Y button</param>
        void EnableFitByY(bool enabled);

       /// <summary>
       /// MVP design View interface
       /// </summary>
       /// <param name="divX">scale step by X</param>
       /// <param name="divY">scale step by Y</param>
       void SetDivisionValues(double divX, double divY);
    }
}
