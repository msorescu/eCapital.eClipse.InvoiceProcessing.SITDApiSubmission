using Moq;
using SITDApiSubmission.UnitTests.Sandbox.Configuration;

namespace SITDApiSubmission.UnitTests.Sandbox;
internal static class SandboxSettings
{
    internal static Mock<IConfig> CreateMockConfig()
    {
        var mockConfig = new Mock<IConfig>();

        mockConfig
            .Setup(m => m.base_url)
            .Returns(() => new Config().base_url);

        mockConfig
            .Setup(m => m.token_url)
            .Returns(() => new Config().token_url);

        mockConfig
           .Setup(m => m.client_id)
           .Returns(() => new Config().client_id);

        mockConfig
           .Setup(m => m.secret_id)
           .Returns(() => new Config().secret_id);

        mockConfig
           .Setup(m => m.ocp_Apim_Subscription_Key)
           .Returns(() => new Config().ocp_Apim_Subscription_Key);

        mockConfig
          .Setup(m => m.ClientConnection)
          .Returns(() => new Config().ClientConnection);

        mockConfig
          .Setup(m => m.ReadOnly)
          .Returns(false);

        return mockConfig;
    }
}
