using SITDApiSubmission.API;
using SITDApiSubmission.Client;
using SITDApiSubmission.Client.Models;
using SITDApiSubmission.UnitTests.Sandbox;
using SITDApiSubmission.UnitTests.Sandbox.Configuration;

namespace SITDApiSubmission.UnitTests.TraffixAPI;
public class TraffixApiUnitTestAbstract
{
    private AuthenticationClient<AccessTokenResponseTraffix> _auth;
    internal string ApiVersion { get; private set; }
    internal string OcpApimSubscriptionKey { get; private set; }
    internal TraffixAPIClient TraffixApiClient { get; private set; }

    IConfig Config { get; }

    public TraffixApiUnitTestAbstract()
    {
        var mockConfig = SandboxSettings.CreateMockConfig();
        Config = mockConfig.Object;
    }

    [SetUp]
    public virtual void SetUp()
    {
        _auth = new AuthenticationClient<AccessTokenResponseTraffix>();

        //Pass in the login information
        _auth.GetUserCredentials(Config.client_id, Config.secret_id, Config.ocp_Apim_Subscription_Key);
        var accessToken = _auth.AccessInfo?.Data?.AccessToken ?? string.Empty;
        var apiVersion = _auth.ApiVersion;
        var baseUrl = Config.base_url;

        ApiVersion = apiVersion;
        OcpApimSubscriptionKey = Config.ocp_Apim_Subscription_Key;

        TraffixApiClient = new TraffixAPIClient(baseUrl, accessToken, apiVersion);
    }

    [TearDown]
    public virtual void TearDown()
    {
        TraffixApiClient?.Dispose();
    }
}
