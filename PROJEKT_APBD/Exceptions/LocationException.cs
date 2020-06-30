using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PROJEKT_APBD.Exceptions
{
    public class LocationException : Exception
    {
        public LocationException()
        {
        }

        public LocationException(string message) : base(message)
        {
        }

        public LocationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LocationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
