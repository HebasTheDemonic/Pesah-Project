using System;
using System.Runtime.Serialization;

namespace OrderManagmentProject
{
    [Serializable]
    internal class StoreHasNoStockException : Exception
    {
        public StoreHasNoStockException()
        {
        }

        public StoreHasNoStockException(string message) : base(message)
        {
        }

        public StoreHasNoStockException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StoreHasNoStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}