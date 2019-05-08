using System;
using System.Runtime.Serialization;

namespace OrderManagmentProject
{
    [Serializable]
    internal class OptionDoesNotExistException : Exception
    {
        public OptionDoesNotExistException()
        {
        }

        public OptionDoesNotExistException(string message) : base(message)
        {
        }

        public OptionDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OptionDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}