using Newtonsoft.Json;

namespace SITDApiSubmission.Client.Models;
public class DataResponseTraffix
{
    // <summary>
    /// ClientId
    /// </summary>
    [JsonProperty(PropertyName = "clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// Expires token on
    /// </summary>
    [JsonProperty(PropertyName = "expiresOn")]
    public DateTime? ExpiresOn { get; set; }

    /// <summary>
    /// Access token that acts as a session ID that the application uses for making requests.
    /// This token should be protected as though it were user credentials.
    /// </summary>
    [JsonProperty(PropertyName = "accessToken")]
    public string? AccessToken { get; set; }
}
