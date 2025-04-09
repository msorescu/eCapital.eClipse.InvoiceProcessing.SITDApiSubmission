namespace SITDApiSubmission.API.Utils;
public static class GetUri
{
    public static Uri BaseFormatUri(string baseUrl, string apiVersion)
    {
        if (string.IsNullOrEmpty(baseUrl))
        {
            throw new ArgumentNullException(nameof(baseUrl));
        }

        if (string.IsNullOrEmpty(apiVersion))
        {
            throw new ArgumentNullException(nameof(apiVersion));
        }

        return new Uri(new Uri(baseUrl), $"/external/api/{apiVersion}/");
    }
}
