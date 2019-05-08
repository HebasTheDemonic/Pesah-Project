using System;
using System.Runtime.Serialization;

namespace OrderManagmentProject
{
    [Serializable]
    internal class SupplierAlreadyExistsException : Exception
    {
        public SupplierAlreadyExistsException()
        {
        }

        public SupplierAlreadyExistsException(string message) : base(message)
        {
        }

        public SupplierAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SupplierAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}