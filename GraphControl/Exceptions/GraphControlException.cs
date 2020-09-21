using System;

namespace GraphControl.Exceptions
{
    public class GraphControlException : Exception
    {
        public GraphControlException(string message) : base(message)
        {
        }

        public GraphControlException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
