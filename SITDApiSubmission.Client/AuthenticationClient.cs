using RestSharp;
using SITDApiSubmission.Client.AccessCredentials;

namespace SITDApiSubmission.Client;
public class AuthenticationClient<T> : IDisposable
{
    private const string TOKEN_REQUEST_ENDPOINT_URL = "https://api-uati.traffix.com/external/api/v1/token";

    private readonly RestClient _restClient;

    private static string DefaultApiVersion => "v1";

    public string ApiVersion { get; set; }

    /// <summary>
    /// The access token response from a successful authentication.
    /// </summary>
    public T? AccessInfo { get; private set; }

    /// <summary>
    /// Initialize the AuthenticationClient with the libary's default Traffix API version
    /// See the DefaultApiVersion property
    /// </summary>
    public AuthenticationClient()
        : this(null)
    {
    }

    /// <summary>
    /// Initialize the AuthenticationClient with the specified Traffix API version
    /// </summary>
    /// <param name="apiVersion"></param>
    /// <param name="tokenRequestEndpointUrl"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="FormatException"></exception>
    public AuthenticationClient(string? apiVersion, string tokenRequestEndpointUrl = TOKEN_REQUEST_ENDPOINT_URL)
    {
        if (!string.IsNullOrEmpty(apiVersion))
        {
            ApiVersion = apiVersion;
        }
        else
        {
            ApiVersion = DefaultApiVersion;
        }

        if (string.IsNullOrEmpty(tokenRequestEndpointUrl))
        {
            throw new ArgumentNullException(nameof(tokenRequestEndpointUrl), "Token Request Endpoint is null or empty");
        }

        if (!Uri.IsWellFormedUriString(tokenRequestEndpointUrl, UriKind.Absolute))
        {
            throw new FormatException("Invalid tokenRequestEndpointUrl");
        }

        var options = new RestClientOptions
        {
            BaseUrl = new Uri(tokenRequestEndpointUrl),
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };

        _restClient = new RestClient(options);
    }

    public void GetUserCredentials(params string[] args)
    {
        var userCredentials = new UserCredentials<T>(_restClient);
        userCredentials.GetUserCredentials(args);
        AccessInfo = userCredentials.AccessInfo;
    }

    public void Dispose()
    {
        _restClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
