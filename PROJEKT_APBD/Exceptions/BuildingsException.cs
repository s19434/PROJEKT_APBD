using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PROJEKT_APBD.Exceptions
{
    public class BuildingsException : Exception
    {
        public BuildingsException()
        {
        }

        public BuildingsException(string message) : base(message)
        {
        }

        public BuildingsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BuildingsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
