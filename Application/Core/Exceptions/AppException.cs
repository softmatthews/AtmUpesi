namespace Application.Core.Exceptions
{
    /// <summary>
    /// Object class for ExceptionMiddleware
    /// </summary>
    /// <seealso cref="ExceptionMiddleware">Exception Middleware</seealso>
    public class AppException
    {
        public AppException(int status, string message, string? details = null)
        {
            Status = status;
            Message = message;
            Details = details;
        }

        public int Status { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }
    }
}