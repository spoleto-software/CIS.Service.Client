using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CIS.Service.Client.Exceptions
{
    /// <summary>
    /// The exception if the object is not found (404 response).
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        /// <summary>
     /// Constructor with the error message.
     /// </summary>
     /// <param name="message">Error message.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NotFoundException class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Constructor with the error message and inner exception.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="innerException">Inner exception.</param>
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
