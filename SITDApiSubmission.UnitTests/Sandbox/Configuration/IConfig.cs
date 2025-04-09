namespace SITDApiSubmission.UnitTests.Sandbox.Configuration;
public interface IConfig
{
    string base_url { get; }
    string token_url { get; }
    string client_id { get; }
    string secret_id { get; }
    string ocp_Apim_Subscription_Key { get; }

    string ClientConnection { get; }
    bool ReadOnly { get; }
}
