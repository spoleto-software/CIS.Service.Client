using System;
using Spoleto.Common.Objects;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The custom exception from WebAPI.
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// Constructor with the error message.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="originalType">The original exception type.</param>
        public ServiceException(string message, WebType originalType)
            : base(message)
        {
            OriginalType = originalType;
        }

        /// <summary>
        /// Constructor with the error message and inner exception.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="originalType">The original exception type.</param>
        /// <param name="innerException">Inner exception.</param>
        public ServiceException(string message, WebType originalType, Exception innerException)
            : base(message, innerException)
        {
            OriginalType = originalType;
        }

        /// <summary>
        /// Gets the original exception type.
        /// </summary>
        public WebType OriginalType { get; }
    }
}
