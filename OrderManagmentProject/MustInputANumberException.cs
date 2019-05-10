using System;
using System.Runtime.Serialization;

namespace OrderManagmentProject
{
    [Serializable]
    internal class MustInputANumberException : Exception
    {
        public MustInputANumberException()
        {
        }

        public MustInputANumberException(string message) : base(message)
        {
        }

        public MustInputANumberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MustInputANumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}