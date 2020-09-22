using System;
using System.Collections.Generic;
using GraphControlCore.Definitions;
using GraphControlCore.Interfaces.Models;

namespace GraphControlCore.Interfaces.Services
{
    public interface IScaleService : ICanvasSizeChanged, IScaleUpdate, IMarginUpdate, IScaleControl
    {
        IScaleState State { get; }

        event EventHandler StateStepUpdated;

        double ToScreen(Axis axis, double value);

        double ToScreenX(double value);

        double ToScreenY(double value);

        double ScaleToScreen(Axis axis, double value);

        double ScaleToScreenX(double value);

        double ScaleToScreenY(double value);

        double ToData(Axis axis, double value);

        double ToDataX(double value);

        double ToDataY(double value);

        double ScaleToData(Axis axis, double value);

        double ScaleToDataX(double value);

        double ScaleToDataY(double value);

        void SetStep(Axis axis, double value);

        void SetStepX(double value);

        void SetStepY(double value);

        bool IsItemVisible(IDataItem item);

        bool IsItemsVisible(ICollection<IDataItem> items);
    }
}
