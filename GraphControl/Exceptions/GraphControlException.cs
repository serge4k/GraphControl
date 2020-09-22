using System;
using System.Runtime.Serialization;

namespace GraphControlCore.Exceptions
{
    [Serializable]
    public class GraphControlException : Exception
    {
        public GraphControlException()
        {
        }

        public GraphControlException(string message) : base(message)
        {
        }

        public GraphControlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GraphControlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
