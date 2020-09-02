using System;
using System.Runtime.Serialization;

namespace Additive.Exceptions
{
    public class ResourceConflictException : Exception
    {
        public ResourceConflictException()
        {
        }

        protected ResourceConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ResourceConflictException(string? message) : base(message)
        {
        }

        public ResourceConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}