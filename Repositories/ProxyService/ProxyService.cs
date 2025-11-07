using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.IdentityModel.Tokens;
using MyApiProject.Interface;
using MyApiProject.Model;
using MyApiProject.Model.DTO;
using MyApiProject.Model.Entites;
using Newtonsoft.Json;

namespace MyApiProject.Repositories.ProxyService;

public class ProxyService(HttpClient httpClient, ILogger<ProxyService> logger) : IProxyInterface
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<ProxyService> _logger = logger;

    // Send request using DTO
    public async Task<HttpResponseDto> SendRequestAsync(SendRequestDto sendRequestDto)
    {

         var stopwatch = Stopwatch.StartNew();
        try
        {
            // Build URI with query parameters
            var url = BuildUrl(sendRequestDto.Url, sendRequestDto.QueryParams);

            // Create HTTP request message
            var request = new HttpRequestMessage(
new HttpMethod(sendRequestDto.Method.ToUpper()),
url
            );

            //Add Headers
            AddHeaders(request, sendRequestDto.Headers);

//add Authentication
            AddAuthentication(request, sendRequestDto.AuthType, sendRequestDto.AuthValue);
 // Add body (for POST, PUT, PATCH)
                if (!string.IsNullOrEmpty(sendRequestDto.Body) && 
                    sendRequestDto.Method.ToUpper() != "GET" && 
                    sendRequestDto.Method.ToUpper() != "DELETE")
                {
                    request.Content = CreateContent(sendRequestDto.Body, sendRequestDto.BodyType);
                }

                // Send the request
                var response = await _httpClient.SendAsync(request);
                stopwatch.Stop();

                // Parse response
 return await ParseRespondAsync(response, stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            
             stopwatch.Stop();
                _logger.LogError(ex, "Error sending HTTP request");

                return new HttpResponseDto
                {
                    StatusCode = 0,
                    StatusDescription = "Error",
                    Body = string.Empty,
                    ResponseTimeMs = stopwatch.ElapsedMilliseconds,
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    

    public Task<HttpResponseDto> SendRequestAsync(HttpModel httpModel)
    {
        throw new NotImplementedException();
    }

    // Build URI with query parameters
    private string BuildUrl(string baseUrl, Dictionary<string, string>? queryParams)
    {
        if (queryParams == null || !queryParams.Any())
            return baseUrl;

        var quesryString = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
        return baseUrl.Contains('?')
              ? $"{baseUrl}&{quesryString}"
              : $"{baseUrl}?{quesryString}";
    }
    // Add headers to request
    //The AddHeaders method’s job is to add all the headers (like authentication tokens, content type, etc.) from a dictionary to an HTTP request message.
    private void AddHeaders(HttpRequestMessage requestMessage, Dictionary<string, string>? headers)
    {
        if (headers == null) return;
        foreach (var header in headers)
        {
            try
            {
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(ex, $"Failed to add header: {header.Key}"); ;
            }
        }

    }

    private void AddAuthentication(HttpRequestMessage message, string authType, string? authValue)
    {
        if (string.IsNullOrEmpty(authValue)) return;
        switch (authType.ToLower())
        {
            case "bearer":
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authValue);
                break;

            case "basic":
                var bytes = Encoding.UTF8.GetBytes(authValue);
                var base64 = Convert.ToBase64String(bytes);
                message.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64);
                break;


            case "apikey":
                // Assuming API key is added as a header (common pattern)
                message.Headers.TryAddWithoutValidation("X-API-Key", authValue);
                break;

        }
    }


    private HttpContent CreateContent(string body, string bodyType)
    {
        return bodyType.ToLower() switch
        {
            "json" => new StringContent(body, Encoding.UTF8, "application/json"),
            "xml" => new StringContent(body, Encoding.UTF8, "application/xml"),
            "form" => new FormUrlEncodedContent(
                JsonConvert.DeserializeObject<Dictionary<string, string>>(body)
                ?? new Dictionary<string, string>()
            ),
            "raw" or _ => new StringContent(body, Encoding.UTF8, "text/plain")
        };
    }
        

        //After your HTTP request, this method gives you a summary object (HttpResponseDto) that contains:
        private async Task<HttpResponseDto>ParseRespondAsync(HttpResponseMessage response ,long elpasedMs)
    {

        var body = await response.Content.ReadAsStringAsync();
        var headers = new Dictionary<string, string>();

        // Extract headers
        foreach (var header in response.Headers)
        {
            headers[header.Key] = string.Join(", ", header.Value);
        }

        // Extract content headers
        foreach (var header in response.Content.Headers)
        {
            headers[header.Key] = string.Join(", ", header.Value);
        }
        return new HttpResponseDto
        {
            StatusCode = (int)response.StatusCode,
            StatusDescription = response.ReasonPhrase ?? string.Empty,
            Headers = headers,
            Body = body,
            ResponseTimeMs = elpasedMs,
            ContentLength = body.Length,
            IsSuccess = response.IsSuccessStatusCode,
            ErrorMessage = response.IsSuccessStatusCode ? null : response.ReasonPhrase
        };
    }
    
        
    }
