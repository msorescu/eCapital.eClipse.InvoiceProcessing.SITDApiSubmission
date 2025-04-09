using SITDApiSubmission.API.Models;
using SITDApiSubmission.Client.Models;

namespace SITDApiSubmission.API;
public interface ITraffixAPIClient
{
    public Task<CarriersTraffix?> GetAsync(string ocpApimSubscriptionKey, CancellationToken cancellationToken = default);
    public Task<ErrorResponse?> PostAsync(string ocpApimSubscriptionKey, LoadDocumentsTraffix sObject, CancellationToken cancellationToken = default);
}
