using Newtonsoft.Json;
using RestSharp;
using SITDApiSubmission.Client.ErrorHandlers;
using SITDApiSubmission.Client.Models;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace SITDApiSubmission.Client.ApiResponse;
public class ApiResponseTraffix
{
    public async static Task<T?> GetResponse<T>(RestClient restClient, RestRequest request, bool deserializeResponse = true, CancellationToken canncelationToken = default)
    {
        RestResponse? responseMessage;
        try
        {
            responseMessage = await restClient.GetAsync(request, canncelationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            var errMsg = new StringBuilder($"Error sending HTTP request: {ex.Message}");
            if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
            {
                errMsg.Append($" {ex.InnerException.Message}");
            }

            Debug.WriteLine(errMsg.ToString());
            throw new TraffixException(Error.Unknown.ToString(), errMsg.ToString());
        }

        if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new TraffixException(Error.AuthenticationFailure.ToString(), "Authentication failed", responseMessage.StatusCode);
        }

        if (responseMessage.StatusCode == HttpStatusCode.NotFound)
        {
            throw new TraffixException(Error.NotFound.ToString(), "No Carrier or Factor found", responseMessage.StatusCode);
        }

        if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
        {
            throw new TraffixException(Error.UnexpectedError.ToString(), "Unexpected error – please contact TRAFFIX for support", responseMessage.StatusCode);
        }

        //sucessful response, skip deserialization of response content
        if (responseMessage.IsSuccessStatusCode && !deserializeResponse)
        {
            return JsonConvert.DeserializeObject<T>(string.Empty);
        }

        if (responseMessage.Content != null)
        {
            try
            {
                string responseContent = responseMessage.Content;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return string.IsNullOrEmpty(responseContent)
                        ? throw new TraffixException(Error.NoResponseContent.ToString(), "Response content was empty")
                        : JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
            catch (TraffixException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TraffixException(Error.UnexpectedError.ToString(), $"Error parsing response content: {ex.Message}");
            }
        }

        throw new TraffixException(Error.UnexpectedError.ToString(), $"Error processing response: returned {responseMessage?.ErrorMessage} for {request.Resource}");
    }

    public async static Task<T?> PostResponse<T>(RestClient restClient, RestRequest request, bool deserializeResponse = true, CancellationToken canncelationToken = default)
    {
        RestResponse? responseMessage;
        try
        {
            responseMessage = await restClient.PostAsync(request, canncelationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            var errMsg = new StringBuilder($"Error sending HTTP request: {ex.Message}");
            if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
            {
                errMsg.Append($" {ex.InnerException.Message}");
            }

            Debug.WriteLine(errMsg.ToString());
            throw new TraffixException(Error.Unknown.ToString(), errMsg.ToString());
        }

        if (responseMessage.StatusCode == HttpStatusCode.NoContent)
        {
            return JsonConvert.DeserializeObject<T>(string.Empty);
        }

        //sucessful response, skip deserialization of response content
        if (responseMessage.IsSuccessStatusCode && !deserializeResponse)
        {
            return JsonConvert.DeserializeObject<T>(string.Empty);
        }

        if (responseMessage.Content != null)
        {
            try
            {
                string responseContent = responseMessage.Content;

                if (responseMessage.IsSuccessStatusCode
                || responseMessage.StatusCode == HttpStatusCode.NotFound
                || responseMessage.StatusCode == HttpStatusCode.Unauthorized
                || responseMessage.StatusCode == HttpStatusCode.BadRequest
                || responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return string.IsNullOrEmpty(responseContent)
                        ? throw new TraffixException(Error.NoResponseContent.ToString(), "Response content was empty")
                        : JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
            catch (TraffixException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TraffixException(Error.UnexpectedError.ToString(), $"Error parsing response content: {ex.Message}");
            }
        }

        throw new TraffixException(Error.UnexpectedError.ToString(), $"Error processing response: returned {responseMessage?.ErrorMessage} for {request.Resource}");
    }
}
