using SITDApiSubmission.API.Models;
using SITDApiSubmission.Client.Models;

namespace SITDApiSubmission.TraffixApi;

public interface ITraffixHelperApi
{
    public Task<CarriersTraffix?> GetCarrierDetailsAsync(string ocpApimSubscriptionKey, CancellationToken cancellationToken = default);
    public Task<ErrorResponse?> LoadDocumentsAsync(string ocpApimSubscriptionKey, string loadNumber, string carrierVendorId, List<DocumentTraffix> documents, CancellationToken cancellationToken = default);
}

