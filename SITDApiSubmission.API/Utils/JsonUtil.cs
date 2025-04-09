using Newtonsoft.Json;

namespace SITDApiSubmission.API.Utils;
public static class JsonUtil
{
    public static string SerializeForCreate(object? inputObject)
    {
        if (inputObject == null)
        {
            return string.Empty;
        }

        string serializedJson = JsonConvert.SerializeObject(inputObject,
               Formatting.None,
               new JsonSerializerSettings
               {
                   NullValueHandling = NullValueHandling.Ignore,
                   DateFormatString = DateFormats.FullDateFormatString
               });

        return serializedJson;
    }
}
