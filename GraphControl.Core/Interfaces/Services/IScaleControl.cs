using System.Drawing;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IScaleControl
    {
        /// <summary>
        /// Recrtange relateed zoom
        /// </summary>
        /// <param name="rectangle">rectange in screenn coordinates</param>
        /// <param name="increase">true to zoom out, otherwise zoom in</param>
        void Zoom(Rectangle rectangle, bool increase);
        
        /// <summary>
        /// Point related zoom
        /// </summary>
        /// <param name="location">point in screen coordinates</param>
        /// <param name="wheelDelta">if greather than zero - zoom out, oterwise zoom in</param>
        void Zoom(Point location, int wheelDelta);

        /// <summary>
        /// Zoom in or out the scale
        /// </summary>
        /// <param name="wheelDelta">if greather than zero - zoom out, oterwise zoom in</param>
        void Zoom(int wheelDelta);

        /// <summary>
        /// Shift scale to offset in scren coordinates
        /// </summary>
        /// <param name="offsetX">X offset in screen coordinates</param>
        /// <param name="offsetY">Y offset in screen coordinates</param>
        void Move(int offsetX, int offsetY);
    }
}
