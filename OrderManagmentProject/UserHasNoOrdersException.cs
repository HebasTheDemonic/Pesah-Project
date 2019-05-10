using System;
using System.Runtime.Serialization;

namespace OrderManagmentProject
{
    [Serializable]
    internal class UserHasNoOrdersException : Exception
    {
        public UserHasNoOrdersException()
        {
        }

        public UserHasNoOrdersException(string message) : base(message)
        {
        }

        public UserHasNoOrdersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserHasNoOrdersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}