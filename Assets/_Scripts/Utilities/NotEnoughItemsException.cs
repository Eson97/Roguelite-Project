using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class NotEnoughItemsException : Exception
    {
        public NotEnoughItemsException()
        {
        }

        public NotEnoughItemsException(string message) : base(message)
        {
        }

        public NotEnoughItemsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughItemsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
