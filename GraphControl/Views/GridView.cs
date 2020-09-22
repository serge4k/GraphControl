using System;
using System.Drawing;
using GraphControl.Definitions;
using GraphControl.Exceptions;
using GraphControl.Interfaces;
using GraphControl.Interfaces.Models;
using GraphControl.Interfaces.Services;
using GraphControl.Interfaces.Views;
using GraphControl.Models;
using GraphControl.Structs;

namespace GraphControl.Views
{
    public class GridView : IGridView
    {
        public IMargin LabelMargin { get; set; }

        private readonly IGridState state;

        private readonly IScaleService scaleService;

        private readonly IItemFormatter itemFormatter;

        public GridView(IGridState state, IScaleService scaleService, IItemFormatter itemFormatter)
        {
            this.state = state;
            this.scaleService = scaleService;
            this.itemFormatter = itemFormatter;

            this.LabelMargin = new Margin(5,5,5,5);
        }

        public virtual void Draw(IDrawing drawing, DrawOptions options, IMargin margin)
        {
            if (drawing == null || margin == null)
            {
                throw new GraphControlException("parameter is null");
            }
            var canvasSize = options.CanvasSize;
            
            // Step 1 - Measure all texts and calc offsets
            // For bottom offset get X string height for 0 value
            var strValueX = this.itemFormatter.ToString(Axis.X, new DataItem(0, 0), this.scaleService.ScaleToDataX(this.state.MinGridLineDistance));
            var strX = drawing.MeasureText(strValueX);

            SizeF strY = new SizeF();
            // For left offset find max Y string width
            double stepDataY = 0;
            if (this.scaleService.State.Y1 <= 0 && this.scaleService.State.Y2 >= 0)
            {
                DrawHorizontalLines(drawing, canvasSize, margin, ref strY, ref stepDataY, true, false, true);

                DrawHorizontalLines(drawing, canvasSize, margin, ref strY, ref stepDataY, true, true, true);
            }
            else
            {
                DrawHorizontalLines(drawing, canvasSize, margin, ref strY, ref stepDataY, false, false, true);
            }

            // Step 2 - Draw vertical lines
            // Draw axe X and grid from axe X to left and to right
            double stepDataX = 0;
            if (this.scaleService.State.X1 <= 0 && this.scaleService.State.X2 >= 0)
            {
                DrawVerticalLines(drawing, canvasSize, margin, strX, ref stepDataX, true, false);
                DrawVerticalLines(drawing, canvasSize, margin, strX, ref stepDataX, true, true);
            }
            else
            {
                DrawVerticalLines(drawing, canvasSize, margin, strX, ref stepDataX, false, false);
            }

            // Step 3 - Draw horizontal lines
            if (this.scaleService.State.Y1 <= 0 && this.scaleService.State.Y2 >= 0)
            {
                DrawHorizontalLines(drawing, canvasSize, margin, ref strY, ref stepDataY, true, false, false);
                DrawHorizontalLines(drawing, canvasSize, margin, ref strY, ref stepDataY, true, true, false);
            }
            else
            {
                DrawHorizontalLines(drawing, canvasSize, margin, ref strY, ref stepDataY, false, false, false);
            }

            // Step 4 - Draw axis X and Y
            if (this.scaleService.State.X1 < 0 && this.scaleService.State.X2 > 0)
            {
                int lastTextPos = 0;
                DrawVerticalLine(drawing, canvasSize, margin, strX, 0, stepDataX, StringAlignment.Center, ref lastTextPos);
            }
            if (this.scaleService.State.Y1 < 0 && this.scaleService.State.Y2 > 0)
            {
                double maxTextSize = 0;
                int lastTextPos = 0;
                DrawHorizontalLine(drawing, canvasSize, margin, ref strY, 0, stepDataY, false, ref maxTextSize, StringAlignment.Center, ref lastTextPos);
            }

            this.scaleService.SetStepX(stepDataX);
            this.scaleService.SetStepY(stepDataY);
        }

        private void DrawVerticalLines(IDrawing drawing, Structs.Size canvasSize, IMargin margin, SizeF strX, ref double stepDataAbs, bool fromZero, bool back)
        {
            if (stepDataAbs == 0)
            {
                double minDistance = Math.Max(this.state.MinGridLineDistance, strX.Width + this.LabelMargin.LeftAndRight);
                stepDataAbs = ToNearRoundStep(Axis.X, this.scaleService.State.X1, this.scaleService.State.X2, minDistance, canvasSize.Width, this.itemFormatter);
            }

            double stepData = stepDataAbs;
            if (back)
            {
                stepData = -stepDataAbs;
            }

            // Calc border values in data
            double firstData = 0;
            double endData = 0;
            if (fromZero)
            {
                firstData = stepData;
                if (back)
                {
                    endData = this.scaleService.State.X1 + stepDataAbs / 2;
                }
                else
                {
                    endData = this.scaleService.State.X2 - stepDataAbs / 2;
                }
            }
            else
            {
                firstData = ToNearRoundValue(this.scaleService.State.X1, stepDataAbs) + stepDataAbs;
                endData = this.scaleService.State.X2 - stepData;
            }

            int steps = canvasSize.Width;
            int lastTextPos = 0;
            DrawVerticalLine(drawing, canvasSize, margin, strX, this.scaleService.State.X1, stepData, StringAlignment.Center, ref lastTextPos); // First line value
            for (double valueData = firstData; ((!back && valueData < endData) || (back && valueData > endData)) && steps >= 0; valueData += stepData, steps--)
            {
                DrawVerticalLine(drawing, canvasSize, margin, strX, valueData, stepData, StringAlignment.Center, ref lastTextPos);
            }
            if (this.scaleService.State.X2 != this.scaleService.State.X1)
            {
                DrawVerticalLine(drawing, canvasSize, margin, strX, this.scaleService.State.X2, stepData, StringAlignment.Far, ref lastTextPos); // Last line value
            }
        }

        private void DrawVerticalLine(IDrawing drawing, Structs.Size canvasSize, IMargin margin, SizeF strX, double valueData, double stepData, StringAlignment align, ref int lastTextPos)
        {
            double x = this.scaleService.ToScreenX(valueData);
            if (x >= 0 &&
                (align == StringAlignment.Center && (x <= canvasSize.Width - margin.Left - margin.Right - strX.Width / 2) ||
                (align == StringAlignment.Far && (x - strX.Width <= canvasSize.Width - margin.Left - margin.Right))))
            {
                double lineXPos = x + margin.Left;
                var color = valueData != 0 ? this.state.GridColor : this.state.AxeColor;
                drawing.Line(color, lineXPos, margin.Top, lineXPos, canvasSize.Height - margin.Bottom);

                var strValue = this.itemFormatter.ToString(Axis.X, new DataItem(valueData, 0), stepData);
                var textX = (int)(lineXPos - strX.Width / 2 - this.LabelMargin.Left);
                var textY = (int)(canvasSize.Height - margin.Bottom + this.LabelMargin.Top);
                var rect = new Rectangle();
                if (align == StringAlignment.Far)
                {
                    // Align right
                    rect = new Rectangle((int)(lineXPos - strX.Width - this.LabelMargin.LeftAndRight), textY, (int)(strX.Width + this.LabelMargin.LeftAndRight), (int)(strX.Height + this.LabelMargin.TopAndBottom));
                }
                else
                {
                    // Align center
                    rect = new Rectangle(textX, textY, (int)(strX.Width + this.LabelMargin.Left + this.LabelMargin.Right), (int)(strX.Height + this.LabelMargin.Top + this.LabelMargin.Bottom));
                }
                if ((rect.Left >= lastTextPos && // Do not draw label if it will be overlapped
                     rect.Right <= canvasSize.Width - margin.Right - this.LabelMargin.LeftAndRight - strX.Width) // Check also right max value
                    || valueData == this.scaleService.State.X2) // Always draw max scale value
                {
                    double linePos2 = canvasSize.Height - margin.Bottom;
                    drawing.Line(color, lineXPos, linePos2, lineXPos, linePos2 + this.LabelMargin.Top + 1);
                    drawing.Text(this.state.TextXColor, rect, strValue, align, StringAlignment.Center);
                    lastTextPos = rect.Right;
                }
            }
        }

        private void DrawHorizontalLines(IDrawing drawing, Structs.Size canvasSize, IMargin margin, ref SizeF strY, ref double stepDataAbs, bool fromZero, bool back, bool calcWidth)
        {
            if (stepDataAbs == 0)
            {
                stepDataAbs = ToNearRoundStep(Axis.Y, this.scaleService.State.Y1, this.scaleService.State.Y2, this.state.MinGridLineDistance, canvasSize.Height, this.itemFormatter);
            }

            double stepData = stepDataAbs;
            if (back)
            {
                stepData = -stepDataAbs;
            }

            // Calc border values in data
            double firstData = 0;
            double endData = 0;
            if (fromZero)
            {
                firstData = stepData;
                if (back)
                {
                    endData = this.scaleService.State.Y1 + stepDataAbs / 2;
                }
                else
                {
                    endData = this.scaleService.State.Y2 - stepDataAbs / 2;
                }
            }
            else
            {
                firstData = ToNearRoundValue(this.scaleService.State.Y1, stepDataAbs) + stepDataAbs;
                endData = this.scaleService.State.Y2 - stepData;
            }

            double maxTextSize = 0;
            int steps = canvasSize.Width;
            int lastTextPos = -1;
            DrawHorizontalLine(drawing, canvasSize, margin, ref strY, this.scaleService.State.Y1, stepData, calcWidth, ref maxTextSize, StringAlignment.Far, ref lastTextPos); // First line value
            for (double valueData = firstData; ((!back && valueData < endData) || (back && valueData > endData)) && steps >= 0; valueData += stepData, steps--)
            {
                DrawHorizontalLine(drawing, canvasSize, margin, ref strY, valueData, stepData, calcWidth, ref maxTextSize, StringAlignment.Center, ref lastTextPos);
            }
            if (this.scaleService.State.Y2 != this.scaleService.State.Y1)
            {
                DrawHorizontalLine(drawing, canvasSize, margin, ref strY, this.scaleService.State.Y2, stepData, calcWidth, ref maxTextSize, StringAlignment.Near, ref lastTextPos); // Last line value
            }
            if (calcWidth)
            {
                strY.Width = (float)maxTextSize > strY.Width ? (float)maxTextSize : strY.Width;
            }
        }

        private void DrawHorizontalLine(IDrawing drawing, Structs.Size canvasSize, IMargin margin, ref SizeF strY, double valueData, double stepData, bool calcWidth, ref double maxTextSize, StringAlignment align, ref int lastTextPos)
        {
            double y = this.scaleService.ToScreenY(valueData);
            if (y >= 0 && y <= canvasSize.Height - margin.Bottom - margin.Top)
            {
                if (calcWidth)
                {
                    var strValue = this.itemFormatter.ToString(Axis.Y, new DataItem(0, valueData), stepData);
                    var strSize = drawing.MeasureText(strValue);
                    maxTextSize = maxTextSize < strSize.Width ? strSize.Width : maxTextSize;
                    strY.Height = strSize.Height;
                }
                else
                {
                    double lineYPos = canvasSize.Height - margin.Bottom - y; // Invert graphic
                    var color = valueData != 0 ? this.state.GridColor : this.state.AxeColor;
                    drawing.Line(color, margin.Left, lineYPos, canvasSize.Width - margin.Right, lineYPos);
                    var strValue = this.itemFormatter.ToString(Axis.Y, new DataItem(0, valueData), stepData);
                    var textX = (int)this.LabelMargin.Left;
                    var textY = (int)(lineYPos - (strY.Height + this.LabelMargin.TopAndBottom) / 2);
                    var textWidth = (int)(margin.Left - this.LabelMargin.LeftAndRight);
                    var textHeight = (int)(strY.Height + this.LabelMargin.TopAndBottom);
                    Rectangle rect;
                    if (align == StringAlignment.Far)
                    {
                        // Align bottom
                        rect = new Rectangle(textX, (int)(lineYPos - strY.Height - this.LabelMargin.Bottom), textWidth, textHeight);
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
                    bool borderValue = valueData == this.scaleService.State.Y1 || valueData == this.scaleService.State.Y2;
                    if (borderValue || (rect.Top > margin.Top + (strY.Height + this.LabelMargin.TopAndBottom) / 2 && rect.Bottom < canvasSize.Height - strY.Width - this.LabelMargin.TopAndBottom))
                    {
                        drawing.Text(this.state.TextYColor, rect, strValue, StringAlignment.Far, align);
                        lastTextPos = rect.Top;
                    }
                }
            }
        }

        private double ToNearRoundStep(Axis axis, double v1, double v2, double minGridLineDistance, int screenRange, IItemFormatter formatter)
        {
            double dataRange = v2 - v1;
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

        private static double ToNearRoundValue(double value, double step)
        {
            return Math.Floor(value / step) * step;
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
