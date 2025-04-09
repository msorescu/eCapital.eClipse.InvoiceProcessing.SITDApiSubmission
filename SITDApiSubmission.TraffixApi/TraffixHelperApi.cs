using SITDApiSubmission.API;
using SITDApiSubmission.API.Models;
using SITDApiSubmission.Client;
using SITDApiSubmission.Client.Models;

namespace SITDApiSubmission.TraffixApi;

public class TraffixHelperApi : ITraffixHelperApi, IDisposable
{
    private TraffixAPIClient _traffixAPIClient;
    private AuthenticationClient<AccessTokenResponseTraffix> _auth;

    public TraffixHelperApi(string baseUrl, params string[] args)
    {
        InitClient(baseUrl, args);
    }

    public string OcpApimSubscriptionKey { get; set; }

    public string TraffixAuthToken { get; set; }

    private void InitClient(string baseUrl, params string[] args)
    {
        if (_traffixAPIClient == null)
        {
            try
            {
                _auth = new AuthenticationClient<AccessTokenResponseTraffix>();

                //Pass in the login information
                _auth.GetUserCredentials(args);
                var accessToken = _auth.AccessInfo?.Data?.AccessToken ?? string.Empty;
                var apiVersion = _auth.ApiVersion;

                _traffixAPIClient ??= new TraffixAPIClient(baseUrl, accessToken, apiVersion);

                TraffixAuthToken = accessToken;
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot login to Traffix because:{ex.Message}");
            }
        }
    }

    public async Task<CarriersTraffix?> GetCarrierDetailsAsync(string ocpApimSubscriptionKey, CancellationToken cancellationToken = default)
    {
        return await _traffixAPIClient.GetAsync(ocpApimSubscriptionKey, cancellationToken).ConfigureAwait(true);
    }

    public async Task<ErrorResponse?> LoadDocumentsAsync(
        string ocpApimSubscriptionKey,
        string loadNumber,
        string carrierVendorId,
        List<DocumentTraffix> documents,
        CancellationToken cancellationToken = default)
    {
        var newDocument = new LoadDocumentsTraffix
        {
            LoadNumber = loadNumber,
            CarrierVendorId = carrierVendorId,
            Documents = documents
        };

        return await _traffixAPIClient.PostAsync(ocpApimSubscriptionKey, newDocument, cancellationToken).ConfigureAwait(true);
    }

    public void Dispose()
    {
        _traffixAPIClient?.Dispose();
        GC.SuppressFinalize(this);
    }
}

