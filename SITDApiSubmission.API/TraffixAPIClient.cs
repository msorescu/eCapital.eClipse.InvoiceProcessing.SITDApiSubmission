using RestSharp;
using RestSharp.Authenticators.OAuth2;
using SITDApiSubmission.API.Models;
using SITDApiSubmission.API.Utils;
using SITDApiSubmission.Client.ApiResponse;
using SITDApiSubmission.Client.Models;

namespace SITDApiSubmission.API;

public class TraffixAPIClient : ITraffixAPIClient, IDisposable
{
    private const string OCP_APIM_SUBSCRIPTION_KEY = "Ocp-Apim-Subscription-Key";

    private readonly RestClient _traffixAPIClient;

    public TraffixAPIClient(string baseUrl, string accessToken, string apiVersion) : this(
        GetUri.BaseFormatUri(baseUrl, apiVersion),
        accessToken)
    {
    }
    public TraffixAPIClient(Uri baseUri, string accessToken) : this(
        accessToken,
        new RestClientOptions(baseUri.ToString()))
    {
    }

    public TraffixAPIClient(string accessToken, RestClientOptions restClientOptions)
    {
        _traffixAPIClient = new RestClient(restClientOptions)
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer")
        };
    }

    public Task<CarriersTraffix?> GetAsync(string ocpApimSubscriptionKey, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(ocpApimSubscriptionKey))
        {
            throw new ArgumentNullException(OCP_APIM_SUBSCRIPTION_KEY, "Ocp-Apim-Subscription-Key is null or empty");
        }

        var request = new RestRequest(CarriersTraffix.TResourceTypeName, Method.Get).AddHeader(
            OCP_APIM_SUBSCRIPTION_KEY,
            ocpApimSubscriptionKey);
        return ApiResponseTraffix.GetResponse<CarriersTraffix>(_traffixAPIClient, request, true, cancellationToken);
    }

    public Task<ErrorResponse?> PostAsync(string ocpApimSubscriptionKey, LoadDocumentsTraffix sObject, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(ocpApimSubscriptionKey))
        {
            throw new ArgumentNullException(OCP_APIM_SUBSCRIPTION_KEY, "Ocp-Apim-Subscription-Key is null or empty");
        }

        var json = JsonUtil.SerializeForCreate(sObject);
        var request = new RestRequest($"{LoadDocumentsTraffix.TResourceTypeName}/", Method.Post).AddHeader(
            OCP_APIM_SUBSCRIPTION_KEY,
            ocpApimSubscriptionKey)
            .AddJsonBody(json);
        return ApiResponseTraffix.PostResponse<ErrorResponse>(_traffixAPIClient, request, true, cancellationToken);
    }

    public void Dispose()
    {
        _traffixAPIClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
