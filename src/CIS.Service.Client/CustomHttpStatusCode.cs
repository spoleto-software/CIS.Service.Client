namespace CIS.Service.Client
{
    /// <summary>
    /// Contains the values of custom status codes defined for HTTP.
    /// </summary>
    public enum CustomHttpStatusCode
    {
        /// <summary>
        /// Equivalent to HTTP status 900. Indicates that the response contains the exception that was packed into <see cref="ExceptionContent"/>.
        /// </summary>
        ExceptionContent = 900
    }
}
