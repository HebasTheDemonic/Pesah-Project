using System;
using System.Runtime.Serialization;

namespace OrderManagmentProject
{
    [Serializable]
    internal class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException()
        {
        }

        public ProductAlreadyExistsException(string message) : base(message)
        {
        }

        public ProductAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}