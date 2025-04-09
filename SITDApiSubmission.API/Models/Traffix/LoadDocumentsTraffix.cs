using Newtonsoft.Json;

namespace SITDApiSubmission.API.Models;
public class LoadDocumentsTraffix
{
    [JsonIgnore]
    public static string TResourceTypeName
    {
        get { return "documents"; }
    }

    [JsonProperty(PropertyName = "loadNumber")]
    public string? LoadNumber { get; set; }

    [JsonProperty(PropertyName = "carrierVendorId")]
    public string? CarrierVendorId { get; set; }

    [JsonProperty(PropertyName = "documents")]
    public List<DocumentTraffix>? Documents { get; set; }
}
