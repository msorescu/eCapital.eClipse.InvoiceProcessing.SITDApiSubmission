using Newtonsoft.Json;

namespace SITDApiSubmission.API.Models;
public class CarriersTraffix
{
    [JsonIgnore]
    public static string TResourceTypeName
    {
        get { return "carrierdetails"; }
    }

    [JsonProperty(PropertyName = "statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string? Message { get; set; }

    [JsonProperty(PropertyName = "data")]
    public CarriersDataTraffix? Data { get; set; }
}
