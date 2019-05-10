using System;
using System.Runtime.Serialization;

namespace OrderManagmentProject
{
    [Serializable]
    internal class InputIsOutOfBoundsException : Exception
    {
        public InputIsOutOfBoundsException()
        {
        }

        public InputIsOutOfBoundsException(string message) : base(message)
        {
        }

        public InputIsOutOfBoundsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InputIsOutOfBoundsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}