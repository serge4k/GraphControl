using System;
using System.Drawing;
using GraphControl.Core.Definitions;
using GraphControl.Core.Exceptions;
using GraphControl.Core.Interfaces;
using GraphControl.Core.Interfaces.Models;
using GraphControl.Core.Interfaces.Services;
using GraphControl.Core.Interfaces.Views;
using GraphControl.Core.Models;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Views
{
    public class GridView : IGridView
    {
        #region Private fields
        private readonly IScaleService scaleService;
        #endregion

        #region Constructors
        public GridView(IScaleService scaleService)
        {
            if (scaleService == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }
            this.scaleService = scaleService;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Draws the view
        /// </summary>
        /// <param name="drawing">drawing wrapper</param>
        /// <param name="options">drawing options</param>
        /// <param name="margin">drawing margin</param>
        public virtual void Draw(IDrawing drawing, IDrawOptions options, IMargin margin)
        {
            if (drawing == null || margin == null)
            {
                throw new InvalidArgumentException("parameter is null");
            }

            if (!(options is GridDrawOptions))
            {
                throw new InvalidArgumentException("options is not compatible");
            }
            var state = ((GridDrawOptions)options).State;

            var canvasSize = options.CanvasSize;

            // Step 1 - Measure all texts and calc offsets
            MeashureLabelSizes(drawing, margin, state, canvasSize, out SizeF strX, out SizeF strY, out double stepDataY);

            // Step 2 - Draw vertical lines
            DrawVerticalLines(drawing, margin, state, canvasSize, strX, out double stepDataX);

            // Step 3 - Draw horizontal lines
            DrawHorizontalLines(drawing, margin, state, canvasSize, ref strY, ref stepDataY);

            // Step 4 - Draw axis X and Y
            DrawAxis(drawing, margin, state, canvasSize, strX, strY, stepDataY, stepDataX);

            this.scaleService.SetStepX(stepDataX);
            this.scaleService.SetStepY(stepDataY);
        }
        #endregion

        #region Private methods
        private void MeashureLabelSizes(IDrawing drawing, IMargin margin, IGridState state, Structs.Size canvasSize, out SizeF strX, out SizeF strY, out double stepDataY)
        {
            // For bottom offset get X string height for 0 value
            var strValueX = state.ItemFormatter.ToString(Axis.X, new DataItem(0, 0), this.scaleService.ScaleToDataX(state.MinGridLineDistance));
            strX = drawing.MeasureText(strValueX);
            strY = new SizeF();
            // For left offset find max Y string width
            stepDataY = 0;
            if (this.scaleService.State.Y1 <= 0 && this.scaleService.State.Y2 > 0)
            {
                DrawHorizontalLines(state, drawing, canvasSize, margin, ref strY, ref stepDataY, true, false, true);

                DrawHorizontalLines(state, drawing, canvasSize, margin, ref strY, ref stepDataY, true, true, true);
            }
            else if (this.scaleService.State.Y1 != this.scaleService.State.Y2)
            {
                DrawHorizontalLines(state, drawing, canvasSize, margin, ref strY, ref stepDataY, false, false, true);
            }
        }
        
        private void DrawAxis(IDrawing drawing, IMargin margin, IGridState state, Structs.Size canvasSize, SizeF strX, SizeF strY, double stepDataY, double stepDataX)
        {
            if (this.scaleService.State.X1 < 0 && this.scaleService.State.X2 > 0)
            {
                int lastTextPos = 0;
                double valueData = 0;
                double maxData = this.scaleService.State.X2;
                double screenPos = this.scaleService.ToScreenX(valueData);
                DrawVerticalLine(state, drawing, canvasSize, margin, strX, screenPos, valueData, maxData, stepDataX, StringAlignment.Center, ref lastTextPos);
            }
            if (this.scaleService.State.Y1 < 0 && this.scaleService.State.Y2 > 0)
            {
                double maxTextSize = 0;
                int lastTextPos = 0;
                DrawHorizontalLine(state, drawing, canvasSize, margin, ref strY, 0, stepDataY, false, ref maxTextSize, StringAlignment.Center, ref lastTextPos);
            }
        }

        private void DrawVerticalLines(IDrawing drawing, IMargin margin, IGridState state, Structs.Size canvasSize, SizeF strX, out double stepDataX)
        {
            // Draw axe X and grid from axe X to left and to right
            stepDataX = 0;
            if (this.scaleService.State.X1 <= 0 && this.scaleService.State.X2 > 0)
            {
                DrawVerticalLines(state, drawing, canvasSize, margin, strX, ref stepDataX, true, false);
                DrawVerticalLines(state, drawing, canvasSize, margin, strX, ref stepDataX, true, true);
            }
            else if (this.scaleService.State.X1 != this.scaleService.State.X2)
            {
                DrawVerticalLines(state, drawing, canvasSize, margin, strX, ref stepDataX, false, false);
            }
        }

        private void DrawVerticalLines(IGridState state, IDrawing drawing, Structs.Size canvasSize, IMargin margin, SizeF strX, ref double stepDataAbs, bool fromZero, bool back)
        {
            if (stepDataAbs == 0)
            {
                double minDistance = Math.Max(state.MinGridLineDistance, strX.Width + state.LabelPadding.LeftAndRight);
                stepDataAbs = ToNearRoundStep(Axis.X, this.scaleService.State.X1, this.scaleService.State.X2, minDistance, canvasSize.Width, state.ItemFormatter);
            }

            double stepData = back ? -stepDataAbs : stepDataAbs; // If we show lines from 0 and backward, change step sign
            
            // Calc first data grid line
            CalcBorderValues(this.scaleService.State.X1, this.scaleService.State.X2, stepDataAbs, fromZero, back, stepData,
                out double firstData, out double endData);

            int steps = canvasSize.Width;
            int lastTextPos = 0;
            double minData = this.scaleService.State.X1;
            double maxData = this.scaleService.State.X2;
            double screenPos = this.scaleService.ToScreenX(minData);
            DrawVerticalLine(state, drawing, canvasSize, margin, strX, screenPos, minData, maxData, stepData, StringAlignment.Center, ref lastTextPos); // First line value
            for (double valueData = firstData; ((!back && valueData < endData) || (back && valueData > endData)) && steps >= 0; valueData += stepData, steps--)
            {
                screenPos = this.scaleService.ToScreenX(valueData);
                DrawVerticalLine(state, drawing, canvasSize, margin, strX, screenPos, valueData, maxData, stepData, StringAlignment.Center, ref lastTextPos);
            }
            if (this.scaleService.State.X2 != this.scaleService.State.X1)
            {
                screenPos = this.scaleService.ToScreenX(maxData);
                DrawVerticalLine(state, drawing, canvasSize, margin, strX, screenPos, maxData, maxData, stepData, StringAlignment.Far, ref lastTextPos); // Last line value
            }
        }
        
        private static void CalcBorderValues(double firstValue, double endValue, double stepDataAbs, bool fromZero, bool back, double stepData,
            out double firstData, out double endData)
        {
            firstData = 0;
            endData = 0;
            if (fromZero)
            {
                firstData = stepData;
                if (back)
                {
                    endData = firstValue + stepDataAbs / 2;
                }
                else
                {
                    endData = endValue - stepDataAbs / 2;
                }
            }
            else
            {
                firstData = ToNearRoundValue(firstValue, stepDataAbs) + stepDataAbs;
                endData = endValue - stepData;
            }
        }

        private static void DrawVerticalLine(IGridState state, IDrawing drawing, Structs.Size canvasSize, IMargin margin, SizeF strX, double screenPos, double valueData, double maxData, double stepData, StringAlignment align, ref int lastTextPos)
        {
            if (screenPos >= 0 &&
                (align == StringAlignment.Center && (screenPos <= canvasSize.Width - margin.Left - margin.Right - strX.Width / 2) ||
                (align == StringAlignment.Far && (screenPos - strX.Width <= canvasSize.Width - margin.Left - margin.Right))))
            {
                double lineXPos = screenPos + margin.Left;
                var color = valueData != 0 ? state.GridColor : state.AxeColor;
                drawing.Line(color, lineXPos, margin.Top, lineXPos, canvasSize.Height - margin.Bottom);

                if (CalcAndCheckLabelRectX(state, ref canvasSize, margin, ref strX, valueData, maxData, align, lineXPos, ref lastTextPos, out Rectangle rect))
                {
                    double linePos2 = canvasSize.Height - margin.Bottom;
                    drawing.Line(color, lineXPos, linePos2, lineXPos, linePos2 + state.LabelPadding.Top + 1);

                    var strValue = state.ItemFormatter.ToString(Axis.X, new DataItem(valueData, 0), stepData);
                    drawing.Text(state.TextXColor, rect, strValue, align, StringAlignment.Center);

                    lastTextPos = rect.Right;
                }
            }
        }

        private static bool CalcAndCheckLabelRectX(IGridState state, ref Structs.Size canvasSize, IMargin margin,
            ref SizeF strX,
            double valueData, 
            double maxData, 
            StringAlignment align,
            double lineXPos,
            ref int lastTextPos,
            out Rectangle rect)
        {
            var textX = (int)(lineXPos - strX.Width / 2 - state.LabelPadding.Left);
            var textY = (int)(canvasSize.Height - margin.Bottom + state.LabelPadding.Top);
            if (align == StringAlignment.Far)
            {
                // Align right
                rect = new Rectangle((int)(lineXPos - strX.Width - state.LabelPadding.LeftAndRight), textY, (int)(strX.Width + state.LabelPadding.LeftAndRight), (int)(strX.Height + state.LabelPadding.TopAndBottom));
            }
            else
            {
                // Align center
                rect = new Rectangle(textX, textY, (int)(strX.Width + state.LabelPadding.Left + state.LabelPadding.Right), (int)(strX.Height + state.LabelPadding.Top + state.LabelPadding.Bottom));
            }

            return (rect.Left >= lastTextPos && // Do not draw label if it will be overlapped
                     rect.Right <= canvasSize.Width - margin.Right - state.LabelPadding.LeftAndRight - strX.Width) // Check also right max value
                    || valueData == maxData; // Always draw max scale value
        }

        private void DrawHorizontalLines(IDrawing drawing, IMargin margin, IGridState state, Structs.Size canvasSize, ref SizeF strY, ref double stepDataY)
        {
            if (this.scaleService.State.Y1 <= 0 && this.scaleService.State.Y2 > 0)
            {
                DrawHorizontalLines(state, drawing, canvasSize, margin, ref strY, ref stepDataY, true, false, false);
                DrawHorizontalLines(state, drawing, canvasSize, margin, ref strY, ref stepDataY, true, true, false);
            }
            else if (this.scaleService.State.Y1 != this.scaleService.State.Y2)
            {
                DrawHorizontalLines(state, drawing, canvasSize, margin, ref strY, ref stepDataY, false, false, false);
            }
        }

        private void DrawHorizontalLines(IGridState state, IDrawing drawing, Structs.Size canvasSize, IMargin margin, ref SizeF strY, ref double stepDataAbs, bool fromZero, bool back, bool calcWidth)
        {
            if (stepDataAbs == 0)
            {
                stepDataAbs = ToNearRoundStep(Axis.Y, this.scaleService.State.Y1, this.scaleService.State.Y2, state.MinGridLineDistance, canvasSize.Height, state.ItemFormatter);
            }

            double stepData = back ? -stepDataAbs : stepDataAbs; // If we show lines from 0 and backward, change step sign

            // Calc border values in data
            CalcBorderValues(this.scaleService.State.Y1, this.scaleService.State.Y2, stepDataAbs, fromZero, back, stepData,
                out double firstData, out double endData);

            double maxTextSize = 0;
            int steps = canvasSize.Width;
            int lastTextPos = -1;
            DrawHorizontalLine(state, drawing, canvasSize, margin, ref strY, this.scaleService.State.Y1, stepData, calcWidth, ref maxTextSize, StringAlignment.Far, ref lastTextPos); // First line value
            for (double valueData = firstData; ((!back && valueData < endData) || (back && valueData > endData)) && steps >= 0; valueData += stepData, steps--)
            {
                DrawHorizontalLine(state, drawing, canvasSize, margin, ref strY, valueData, stepData, calcWidth, ref maxTextSize, StringAlignment.Center, ref lastTextPos);
            }
            if (this.scaleService.State.Y2 != this.scaleService.State.Y1)
            {
                DrawHorizontalLine(state, drawing, canvasSize, margin, ref strY, this.scaleService.State.Y2, stepData, calcWidth, ref maxTextSize, StringAlignment.Near, ref lastTextPos); // Last line value
            }
            if (calcWidth)
            {
                strY.Width = (float)maxTextSize > strY.Width ? (float)maxTextSize : strY.Width;
            }
        }

        private void DrawHorizontalLine(IGridState state, IDrawing drawing, Structs.Size canvasSize, IMargin margin, ref SizeF strY, double valueData, double stepData, bool calcWidth, ref double maxTextSize, StringAlignment align, ref int lastTextPos)
        {
            double y = this.scaleService.ToScreenY(valueData);
            if (y >= 0 && y <= canvasSize.Height - margin.Bottom - margin.Top)
            {
                if (calcWidth)
                {
                    var strValue = state.ItemFormatter.ToString(Axis.Y, new DataItem(0, valueData), stepData);
                    var strSize = drawing.MeasureText(strValue);
                    maxTextSize = maxTextSize < strSize.Width ? strSize.Width : maxTextSize;
                    strY.Height = strSize.Height;
                }
                else
                {
                    double lineYPos = canvasSize.Height - margin.Bottom - y; // Invert graphic
                    var color = valueData != 0 ? state.GridColor : state.AxeColor;
                    drawing.Line(color, margin.Left, lineYPos, canvasSize.Width - margin.Right, lineYPos);

                    if (CalcAndCheckLabelRectY(state, canvasSize, margin, ref strY, valueData, align, lineYPos, out Rectangle rect, out bool borderValue))
                    {
                        var strValue = state.ItemFormatter.ToString(Axis.Y, new DataItem(0, valueData), stepData);
                        drawing.Text(state.TextYColor, rect, strValue, StringAlignment.Far, align);

                        lastTextPos = rect.Top;
                    }
                }
            }
        }

        private bool CalcAndCheckLabelRectY(IGridState state, Structs.Size canvasSize, IMargin margin, ref SizeF strY, double valueData, StringAlignment align, double lineYPos, out Rectangle rect, out bool borderValue)
        {
            var textX = (int)state.LabelPadding.Left;
            var textY = (int)(lineYPos - (strY.Height + state.LabelPadding.TopAndBottom) / 2);
            var textWidth = (int)(margin.Left - state.LabelPadding.LeftAndRight);
            var textHeight = (int)(strY.Height + state.LabelPadding.TopAndBottom);
            if (align == StringAlignment.Far)
            {
                // Align bottom
                rect = new Rectangle(textX, (int)(lineYPos - strY.Height - state.LabelPadding.Bottom), textWidth, textHeight);
            }
            else if (align == StringAlignment.Near)
            {
                // Align top
                rect = new Rectangle(textX, (int)(lineYPos), textWidth, textHeight);
            }
            else
            {
                // Align center
                rect = new Rectangle(textX, textY, textWidth, textHeight);
            }
            borderValue = valueData == this.scaleService.State.Y1 || valueData == this.scaleService.State.Y2;

            return borderValue || (rect.Top > margin.Top + (strY.Height + state.LabelPadding.TopAndBottom) / 2 && rect.Bottom < canvasSize.Height - strY.Width - state.LabelPadding.TopAndBottom);
        }

        /// <summary>
        /// Returns rounded step for scale grid
        /// </summary>
        /// <param name="axis">For X or Y axis</param>
        /// <param name="minDataValue">min value</param>
        /// <param name="maxDatavakue">max value</param>
        /// <param name="minGridLineDistance">minimal step of grid in screen coordinates</param>
        /// <param name="screenRange">canvas width or height</param>
        /// <param name="formatter">Formatter to show values</param>
        /// <returns>rounded step for scale grid</returns>
        private double ToNearRoundStep(Axis axis, double minDataValue, double maxDatavakue, double minGridLineDistance, int screenRange, IItemFormatter formatter)
        {
            double dataRange = maxDatavakue - minDataValue;
            double dataMinStep = dataRange / (screenRange / minGridLineDistance); // log(interval / max lines count, interval / max lines count > 60 ? 60, 10) 
            int order = (int)(Math.Floor(Math.Log10(Math.Abs(dataRange))));
            double maxStep = Math.Pow(10, order);
            var dividers = formatter.GetScaleDivisions(axis, dataMinStep);
            double foundDivider = 1;
            Array.Sort(dividers, new Comparison<double>((i1, i2) => i2.CompareTo(i1)));
            foreach (var divider in dividers)
            {
                var stepScreen = this.scaleService.ScaleToScreen(axis, maxStep / divider);
                if (stepScreen >= minGridLineDistance)
                {
                    foundDivider = divider;
                    break;
                }
            }
            return maxStep / foundDivider;
        }

        /// <summary>
        /// Returns value rouded by step
        /// </summary>
        /// <param name="value"></param>
        /// <param name="step"></param>
        /// <returns>value rouded by step</returns>
        private static double ToNearRoundValue(double value, double step)
        {
            return Math.Floor(value / step) * step;
        }
        #endregion
    }
}
