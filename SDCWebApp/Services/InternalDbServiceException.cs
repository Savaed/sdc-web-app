using System;
using System.Runtime.Serialization;

namespace SDCWebApp.Services
{
    /// <summary>
    /// Exception indicates an internal database service error such as when 
    /// database doesnt exist or table reference is set to null. 
    /// </summary>
    [Serializable]
    public class InternalDbServiceException : Exception
    {
        public InternalDbServiceException() : base() { }

        public InternalDbServiceException(string message) : base(message) { }

        public InternalDbServiceException(string message, Exception innerException) : base(message, innerException) { }

        protected InternalDbServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
