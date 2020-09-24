using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Interfaces.Models
{
    public interface ICanvasSizeChanged
    {
        /// <summary>
        /// Canvas size changed event
        /// </summary>
        /// <param name="options"></param>
        void CanvasSizeChanged(IDrawOptions options);
    }
}
