using GraphControl.Exceptions;
using GraphControl.Interfaces.Models;
using System;

namespace GraphControl.Models
{
    public class GraphControlFormState : IGraphControlFormState
    {
        #region pbulic fields
        public bool FitToScreenByX { get; set; }

        public bool FitToScreenByY { get; set; }

        public bool FitToScreenAlways { get; set; }
        #endregion

        #region Constructors
        public GraphControlFormState()
        {
            // Default values
            this.FitToScreenByX = true;
            this.FitToScreenByY = true;
            this.FitToScreenAlways = true;
        }

        public GraphControlFormState(IGraphControlFormState state)
        {
            if (state == null)
            {
                throw new GraphControlException("parameter is null");
            }
            this.FitToScreenByX = state.FitToScreenByX;
            this.FitToScreenByY = state.FitToScreenByY;
            this.FitToScreenAlways = state.FitToScreenAlways;
        }
        #endregion
    }
}
