using System;
using System.Runtime.Serialization;

namespace AbsenceRegistrationService
{
    [Serializable]
    public class SqlInjectionException : Exception
    {
        public SqlInjectionException()
        {
        }

        public SqlInjectionException(string message) : base(message)
        {
        }

        public SqlInjectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SqlInjectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}