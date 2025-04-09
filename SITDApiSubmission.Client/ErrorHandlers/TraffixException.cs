using System.Net;

namespace SITDApiSubmission.Client.ErrorHandlers;

public class TraffixException : Exception
{
    public string ErrorCode { get; private set; }

    public HttpStatusCode HttpStatusCode { get; private set; }

    public TraffixException(string errorCode, string message)
        : this(errorCode, message, 0)
    {
    }

    public TraffixException(string errorCode, string message, HttpStatusCode httpStatusCode)
        : base(message)
    {
        ErrorCode = errorCode;
        HttpStatusCode = httpStatusCode;
    }
}
