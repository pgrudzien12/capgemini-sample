using System;
using System.Runtime.Serialization;

namespace CapgeminiSample.Exceptions
{
    public class ApplicationLayerException : Exception
    {
        public ApplicationLayerException()
        {
        }

        public ApplicationLayerException(string message) : base(message)
        {
        }

        public ApplicationLayerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApplicationLayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}