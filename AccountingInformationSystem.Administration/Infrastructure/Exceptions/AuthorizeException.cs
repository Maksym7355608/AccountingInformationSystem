using System.Net;

namespace AccountingInformationSystem.Administration.Infrastructure.Exceptions
{
    public class AuthorizeException : Exception
    {
        public AuthorizeException(string? message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
