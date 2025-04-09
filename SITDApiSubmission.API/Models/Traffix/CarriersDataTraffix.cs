using Newtonsoft.Json;

namespace SITDApiSubmission.API.Models;
public class CarriersDataTraffix
{
    [JsonProperty(PropertyName = "callerVendorId")]
    public string? CallerVendorId { get; set; }

    [JsonProperty(PropertyName = "callerName")]
    public string? CallerName { get; set; }

    [JsonProperty(PropertyName = "factoredCarriers")]
    public List<FactoredCarrierTraffix>? FactoredCarriers { get; set; }
}
