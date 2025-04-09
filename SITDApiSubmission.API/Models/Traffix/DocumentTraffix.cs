using Newtonsoft.Json;

namespace SITDApiSubmission.API.Models;
public class DocumentTraffix
{
    [JsonProperty(PropertyName = "fileName")]
    public string? FileName { get; set; }

    [JsonProperty(PropertyName = "data")]
    public string? Data { get; set; }
}
