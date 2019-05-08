using System;
using System.Runtime.Serialization;

namespace OrderManagmentProject
{
    [Serializable]
    internal class SupplierDoesNotExistException : Exception
    {
        public SupplierDoesNotExistException()
        {
        }

        public SupplierDoesNotExistException(string message) : base(message)
        {
        }

        public SupplierDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SupplierDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}