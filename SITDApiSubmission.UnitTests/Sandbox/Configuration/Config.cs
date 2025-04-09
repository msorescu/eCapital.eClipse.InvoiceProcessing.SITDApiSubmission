using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SITDApiSubmission.UnitTests.Sandbox.Configuration;
public class Config : IConfig
{
    IConfigurationRoot config { get; set; }

    ISecretRevealer revealer { get; set; }

    public string base_url =>
        revealer.secrets.Providers.Traffix.base_url;

    public string token_url =>
        revealer.secrets.Providers.Traffix.token_url;

    public string client_id =>
        revealer.secrets.Providers.Traffix.client_id;

    public string secret_id =>
        revealer.secrets.Providers.Traffix.secret_id;

    public string ocp_Apim_Subscription_Key =>
        revealer.secrets.Providers.Traffix.ocp_Apim_Subscription_Key;


    public string ClientConnection =>
        revealer.secrets.ConnectionStrings.ClientConnection;

    public bool ReadOnly =>
        revealer.secrets.ReadOnly;

    public Config()
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        config = builder.Build();

        var services = new ServiceCollection();
        _ = services
            .Configure<AppSecrets>(config.GetSection("Settings"))
            .AddOptions()
            .AddSingleton<ISecretRevealer, SecretRevealer>()
            .BuildServiceProvider();

        var serviceProvider = services.BuildServiceProvider();

        revealer = serviceProvider.GetService<ISecretRevealer>();
    }
}

internal class AppSecrets
{
    public AppConnectionStrings ConnectionStrings { get; set; }

    public AppProviders Providers { get; set; }

    public bool ReadOnly { get; set; }
}

internal class AppConnectionStrings
{
    public string ClientConnection { get; set; }
}

internal class AppProviders
{
    public AppProviderTraffix Traffix { get; set; }
}

internal class AppProviderTraffix
{
    public string base_url { get; set; }

    public string client_id { get; set; }

    public string secret_id { get; set; }
    public string ocp_Apim_Subscription_Key { get; set; }
    public string token_url { get; set; }
}

internal interface ISecretRevealer
{
    AppSecrets secrets { get; }
}

internal class SecretRevealer : ISecretRevealer
{
    public AppSecrets secrets { get; }

    public SecretRevealer(IOptions<AppSecrets> _secrets)
    {
        secrets = _secrets.Value ?? throw new ArgumentNullException(nameof(_secrets));
    }
}
