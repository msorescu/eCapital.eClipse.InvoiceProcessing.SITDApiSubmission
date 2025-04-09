using RestSharp;
using SITDApiSubmission.Client.ApiResponse;

namespace SITDApiSubmission.Client.AccessCredentials;

public class UserCredentials<AccessTokenResponseTraffix>
{
    private readonly RestClient _restClient;
    public AccessTokenResponseTraffix? AccessInfo { get; private set; }
    public UserCredentials(RestClient restClient)
    {
        _restClient = restClient;
    }

    /// <summary>
    /// Authenticate using the credentials auth flow, synchronously
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="secretId"></param>
    /// <param name="ocpApimSubscriptionKey"></param>
    public void GetUserCredentials(params string[] args)
    {
        try
        {
            GetUserCredentialsAsync(args).Wait();
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException != null && ex.InnerExceptions != null && ex.InnerExceptions.Count == 1)
            {
                throw ex.InnerException;
            }

            throw;
        }
    }

    /// <summary>
    /// Authenticate using the credentials auth flow, synchronously
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="secretId"></param>
    /// <param name="ocpApimSubscriptionKey"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private async Task GetUserCredentialsAsync(
        params string[] args)
    {
        if (string.IsNullOrEmpty(args[0]))
        {
            throw new ArgumentNullException("clientId", "Client ID is null or empty");
        }

        if (string.IsNullOrEmpty(args[1]))
        {
            throw new ArgumentNullException("secretId", "Secret is null or empty");
        }

        if (string.IsNullOrEmpty(args[2]))
        {
            throw new ArgumentNullException("Ocp-Apim-Subscription-Key", "Ocp-Apim-Subscription-Key is null or empty");
        }

        var request = new RestRequest { Method = Method.Get, Timeout = -1, };
        _ = request.AddHeader("clientId", args[0]);
        _ = request.AddHeader("secretId", args[1]);
        _ = request.AddHeader("Ocp-Apim-Subscription-Key", args[2]);

        AccessInfo = await ApiResponseTraffix.GetResponse<AccessTokenResponseTraffix>(_restClient, request);
    }
}
