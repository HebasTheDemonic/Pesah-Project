using System;
using System.Runtime.Serialization;

namespace OrderManagmentProject
{
    [Serializable]
    internal class WrongAmountException : Exception
    {
        public WrongAmountException()
        {
        }

        public WrongAmountException(string message) : base(message)
        {
        }

        public WrongAmountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongAmountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}