using GraphControl.Core.Interfaces;
using GraphControl.Core.Structs;

namespace GraphControl.Core.Interfaces.Services
{
    public interface IMarginUpdate
    {
        /// <summary>
        /// Update margin callback
        /// </summary>
        /// <param name="margin">margin</param>
        void UpdateMargin(IMargin margin);
    }
}
