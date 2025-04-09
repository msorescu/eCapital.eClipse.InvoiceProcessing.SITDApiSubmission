using Newtonsoft.Json;

namespace SITDApiSubmission.API.Models;
public class FactoredCarrierTraffix
{
    [JsonProperty(PropertyName = "vendorId")]
    public string? VendorId { get; set; }

    [JsonProperty(PropertyName = "vendorName")]
    public string? VendorName { get; set; }

    [JsonProperty(PropertyName = "mcNumber")]
    public string? MCNumber { get; set; }
}
