using System.Drawing;

namespace GraphControl.Core.Interfaces
{
    public interface IDrawing
    {
        /// <summary>
        /// Draws the line
        /// </summary>
        /// <param name="color">pen color</param>
        /// <param name="x1">position</param>
        /// <param name="y1">position</param>
        /// <param name="x2">position</param>
        /// <param name="y2">position</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Used as coordinate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Used as coordinate")]
        void Line(Color color, double x1, double y1, double x2, double y2);

        /// <summary>
        /// Draws the line
        /// </summary>
        /// <param name="color">pen color</param>
        /// <param name="x1">position</param>
        /// <param name="y1">position</param>
        /// <param name="x2">position</param>
        /// <param name="y2">position</param>
        /// <param name="clipRectangle">clipping area</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Used as coordinate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Used as coordinate")]
        void Line(Color color, double x1, double y1, double x2, double y2, RectangleF clipRectangle);

        /// <summary>
        /// Draws borders of the rectangle
        /// </summary>
        /// <param name="color">pen color</param>
        /// <param name="x">screen coordinate</param>
        /// <param name="y">screen coordinate</param>
        /// <param name="width">size</param>
        /// <param name="height">size</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Used as coordinate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Used as coordinate")]
        void Rectangle(Color color, double x, double y, double width, double height);

        /// <summary>
        /// Draws borders of the rectangle
        /// </summary>
        /// <param name="color">pen color</param>
        /// <param name="x">screen coordinate</param>
        /// <param name="y">screen coordinate</param>
        /// <param name="width">size</param>
        /// <param name="height">size</param>
        /// <param name="clipRectangle">clipping area</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Used as coordinate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Used as coordinate")]
        void Rectangle(Color color, double x, double y, double width, double height, RectangleF clipRectangle);

        /// <summary>
        /// Draws the rectangle
        /// </summary>
        /// <param name="color">pen color</param>
        /// <param name="colorFill">brish color</param>
        /// <param name="x">screen coordinate</param>
        /// <param name="y">screen coordinate</param>
        /// <param name="width">size</param>
        /// <param name="height">size</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Used as coordinate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Used as coordinate")]
        void FillRectangle(Color color, Color colorFill, double x, double y, double width, double height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color">brush color</param>
        /// <param name="x">screen coordinate</param>
        /// <param name="y">screen coordinate</param>
        /// <param name="value">text to draw</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Used as coordinate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Used as coordinate")]
        void Text(Color color, double x, double y, string value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color">brush color</param>
        /// <param name="rect">text bounds</param>
        /// <param name="value">text to draw</param>
        /// <param name="alignment">horizontal alignment</param>
        /// <param name="lineAlignment">vertical alignment</param>
        void Text(Color color, Rectangle rect, string value, StringAlignment alignment, StringAlignment lineAlignment);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color">brush color</param>
        /// <param name="x">screen coordinate</param>
        /// <param name="y">screen coordinate</param>
        /// <param name="radius">radius</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Used as coordinate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Used as coordinate")]
        void Circle(Color color, double x, double y, double radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color">brush color</param>
        /// <param name="x">screen coordinate</param>
        /// <param name="y">screen coordinate</param>
        /// <param name="radius">radius</param>
        /// <param name="clipRectangle">clipping area</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Used as coordinate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Used as coordinate")]
        void Circle(Color color, double x, double y, double radius, RectangleF clipRectangle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns>width and height of text</returns>
        SizeF MeasureText(string text);

        /// <summary>
        /// Draws buffered data in graphics object and clear. Called also in Dispose()
        /// </summary>
        void Flush();
    }
}
