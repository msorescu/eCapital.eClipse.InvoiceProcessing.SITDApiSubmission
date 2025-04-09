using SITDApiSubmission.Client.Models;
using System.Net;

namespace SITDApiSubmission.API.Domains;

public class ApiTraffixResponseDomainService
{
    private const string API_SUBMISSION_PENDING_NOA_REASON = "Pending NOA";
    private const string API_SUBMISSION_REJECTED_REASON = "Rejected - {0}";
    private const string API_SUBMISSION_API_NOT_SET_REASON = "Api Not Set";

    public static string GetNotSentReason(ErrorResponse? response)
    {
        var result = $"{response?.Message}";
        if (new[] { (int)HttpStatusCode.BadRequest, (int)HttpStatusCode.Unauthorized }.Contains(response?.StatusCode ?? 0))
        {
            result = API_SUBMISSION_API_NOT_SET_REASON;
        }
        else if (response?.StatusCode == (int)HttpStatusCode.InternalServerError)
        {
            result = API_SUBMISSION_PENDING_NOA_REASON;
        }
        else if (response?.StatusCode == (int)HttpStatusCode.NotFound)
        {
            result = string.Format(API_SUBMISSION_REJECTED_REASON, response?.Message);
        }

        return result;
    }
}
