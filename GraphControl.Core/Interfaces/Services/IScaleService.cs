using System;
using System.Collections.Generic;
using GraphControl.Core.Definitions;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IScaleService : IScaleUpdate, IMarginUpdate, IScaleControl
    {
        /// <summary>
        /// Scale service state
        /// </summary>
        IScaleState State { get; }
        
        /// <summary>
        /// Step updated event. Notifies about grid step was changed
        /// </summary>
        event EventHandler StateStepUpdated;

        /// <summary>
        /// Calc coordinate in screen poins from data related to minimal visible value
        /// </summary>
        /// <param name="axis">X or Y</param>
        /// <param name="value">data value</param>
        /// <returns>screen related coordinate</returns>
        double ToScreen(Axis axis, double value);

        /// <summary>
        /// Calc coordinate in screen poins from data related to minimal visible value
        /// </summary>
        /// <param name="value">data value</param>
        /// <returns>screen related coordinate</returns>
        double ToScreenX(double value);

        /// <summary>
        /// Calc coordinate in screen poins from data related to minimal visible value
        /// </summary>
        /// <param name="value">data value</param>
        /// <returns>screen related coordinate</returns>
        double ToScreenY(double value);

        /// <summary>
        /// Calc coordinate in screen poins from data value
        /// </summary>
        /// <param name="axis">X or Y</param>
        /// <param name="value">data value</param>
        /// <returns>screen related coordinate</returns>
        double ScaleToScreen(Axis axis, double value);

        /// <summary>
        /// Calc coordinate in screen poins from data value
        /// </summary>
        /// <param name="value">data value</param>
        /// <returns>screen related coordinate</returns>
        double ScaleToScreenX(double value);

        /// <summary>
        /// Calc coordinate in screen poins from data value
        /// </summary>
        /// <param name="value">data value</param>
        /// <returns>screen related coordinate</returns>
        double ScaleToScreenY(double value);

        /// <summary>
        /// Returns data value from screen related coordinate
        /// </summary>
        /// <param name="axis">X oR Y</param>
        /// <param name="value">screen related coordinate</param>
        /// <returns>data related value including screen minimal visible value</returns>
        double ToData(Axis axis, double value);

        /// <summary>
        /// Returns data value from screen related coordinate
        /// </summary>
        /// <param name="value">screen related coordinate</param>
        /// <returns>data related value including screen minimal visible value</returns>
        double ToDataX(double value);

        /// <summary>
        /// Returns data value from screen related coordinate
        /// </summary>
        /// <param name="value">screen related coordinate</param>
        /// <returns>data related value including screen minimal visible value</returns>
        double ToDataY(double value);

        /// <summary>
        /// Returns data value from screen related coordinate without adding minimal visible value
        /// </summary>
        /// <param name="axis">X oR Y</param>
        /// <param name="value">screen related coordinate</param>
        /// <returns>data related value without adding minimal visible value</returns>
        double ScaleToData(Axis axis, double value);

        /// <summary>
        /// Returns data value from screen related coordinate without adding minimal visible value
        /// </summary>
        /// <param name="value">screen related coordinate</param>
        /// <returns>data related value without adding minimal visible value</returns>
        double ScaleToDataX(double value);

        /// <summary>
        /// Returns data value from screen related coordinate without adding minimal visible value
        /// </summary>
        /// <param name="value">screen related coordinate</param>
        /// <returns>data related value without adding minimal visible value</returns>
        double ScaleToDataY(double value);

        /// <summary>
        /// Updates grid step
        /// </summary>
        /// <param name="axis">X or Y</param>
        /// <param name="value">grid step</param>
        void SetStep(Axis axis, double value);

        /// <summary>
        /// Updates grid step
        /// </summary>
        /// <param name="value">grid step</param>
        void SetStepX(double value);

        /// <summary>
        /// Updates grid step
        /// </summary>
        /// <param name="value">grid step</param>
        void SetStepY(double value);

        /// <summary>
        /// Check that item is visible by min and max visible X,Y values
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsItemVisible(IDataItem item);

        /// <summary>
        /// Check that one of items is visible by min and max visible X,Y values
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsItemsVisible(ICollection<IDataItem> items);
    }
}
