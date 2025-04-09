
using Newtonsoft.Json;

namespace SITDApiSubmission.Client.Models;
public class AccessTokenResponseTraffix
{
    [JsonProperty(PropertyName = "statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string? Message { get; set; }

    [JsonProperty(PropertyName = "data")]
    public DataResponseTraffix? Data { get; set; }
}
