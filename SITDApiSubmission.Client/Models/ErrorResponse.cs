using Newtonsoft.Json;

namespace SITDApiSubmission.Client.Models;
public class ErrorResponse
{
    [JsonProperty(PropertyName = "statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string? Message { get; set; }
}
