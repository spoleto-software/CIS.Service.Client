using System;
using System.Text.Json.Serialization;
using Spoleto.Common.Objects;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The class for passing exception information through Web services.
    /// </summary>
    public class ExceptionContent
    {
        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        [JsonPropertyName("ExceptionMessage")]
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the exception type.
        /// </summary>
        [JsonPropertyName("ExceptionType")]
        public WebType ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets the inner exception.
        /// </summary>
        [JsonPropertyName("InnerException")]
        public ExceptionContent InnerException { get; set; }


        /// <summary>
        /// User-defined conversion from ExceptionContent to ServiceException 
        /// </summary>
        public static explicit operator ServiceException(ExceptionContent exceptionContent)
        {
            if (exceptionContent == null)
            {
                return null;
            }

            ServiceException exception;
            if (exceptionContent.InnerException == null)
            {
                exception = new ServiceException(exceptionContent.ExceptionMessage, exceptionContent.ExceptionType);
            }
            else
            {
                exception = new ServiceException(exceptionContent.ExceptionMessage, exceptionContent.ExceptionType,
                    new ServiceException(exceptionContent.InnerException.ExceptionMessage, exceptionContent.InnerException.ExceptionType));
            }

            return exception;
        }

        /// <summary>
        /// User-defined conversion from ExceptionContent to Exception 
        /// </summary>
        public static explicit operator Exception(ExceptionContent exceptionContent)
        {
            if (exceptionContent == null)
            {
                return null;
            }

            Exception exception;
            if (exceptionContent.InnerException == null)
            {
                try
                {
                    var realType = (Type)exceptionContent.ExceptionType;

                    if (realType == null)
                        exception = new ServiceException(exceptionContent.ExceptionMessage, exceptionContent.ExceptionType);
                    else
                        exception = (Exception)Activator.CreateInstance(realType, exceptionContent.ExceptionMessage);
                }
                catch
                {
                    exception = new Exception(exceptionContent.ExceptionMessage);
                }
            }
            else
            {
                try
                {
                    var realType = (Type)exceptionContent.ExceptionType;
                    var innerRealType = (Type)exceptionContent.InnerException.ExceptionType;

                    if (realType == null || innerRealType == null)
                        exception = new ServiceException(exceptionContent.ExceptionMessage, exceptionContent.ExceptionType,
                            new ServiceException(exceptionContent.InnerException.ExceptionMessage, exceptionContent.InnerException.ExceptionType));
                    else
                        exception = (Exception)Activator.CreateInstance((Type)exceptionContent.ExceptionType, exceptionContent.ExceptionMessage,
                        (Exception)Activator.CreateInstance((Type)exceptionContent.InnerException.ExceptionType, exceptionContent.InnerException.ExceptionMessage));
                }
                catch
                {
                    try
                    {
                        exception = new Exception(exceptionContent.ExceptionMessage,
                            (Exception)Activator.CreateInstance((Type)exceptionContent.InnerException.ExceptionType, exceptionContent.InnerException.ExceptionMessage));
                    }
                    catch
                    {
                        exception = new Exception(exceptionContent.ExceptionMessage, new Exception(exceptionContent.InnerException.ExceptionMessage));
                    }
                }
            }

            return exception;
        }

        /// <summary>
        /// User-defined conversion from Exception to ExceptionContent 
        /// </summary>
        public static explicit operator ExceptionContent(Exception exception)
        {
            if (exception == null)
            {
                return null;
            }

            //todo: use ExceptionManager from new Nuget Core.Common when it's ready!
            //exception =  ExceptionManager.GetRealException(exception);
            var innerException = exception.InnerException != null ? exception.InnerException : null;// ExceptionManager.GetRealException(exception.InnerException) : null;

            return new ExceptionContent
            {
                ExceptionMessage = exception.Message,
                ExceptionType = (WebType)exception.GetType(),
                InnerException = innerException != null
                    ? new ExceptionContent { ExceptionMessage = innerException.Message, ExceptionType = (WebType)innerException.GetType() }
                    : null
            };
        }

        /// <summary>
        /// Returns the string representation.
        /// </summary>
        /// <returns></returns>
        public override String ToString() => ExceptionMessage;
    }
}
