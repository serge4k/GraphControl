using GraphControl.Core.Interfaces.Views;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IScaleUpdate
    {
        /// <summary>
        /// Update scale interface
        /// </summary>
        /// <param name="options">drawing options</param>
        void UpdateScale(IDrawOptions options);
    }
}