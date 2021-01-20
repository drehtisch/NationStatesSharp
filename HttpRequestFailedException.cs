using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NationStatesSharp
{
    public class HttpRequestFailedException : Exception
    {
        public HttpRequestFailedException()
        {
        }

        public HttpRequestFailedException(string message) : base(message)
        {
        }

        public HttpRequestFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpRequestFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}