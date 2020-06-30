using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PROJEKT_APBD.Exceptions
{
    public class PasswordException : Exception 
    {
        public PasswordException()
        {
        }

        public PasswordException(string message) : base(message)
        {
        }

        public PasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
