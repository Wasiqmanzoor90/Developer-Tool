using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using MyApiProject.Interface;
using MyApiProject.Model;
using MyApiProject.Model.DTO;
using MyApiProject.Model.Entites;

namespace MyApiProject.Repositories.ProxyService;

public class ProxyService(HttpClient httpClient, ILogger<ProxyService> logger) : IProxyInterface
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<ProxyService> _logger = logger;

    // Send request using DTO
    public Task<SendRequestDto> SendRequestAsync(SendRequestDto sendRequestDto)
    {
        throw new NotImplementedException();
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
    private void AddHeaders(HttpRequestMessage requestMessage,  Dictionary<string, string>? headers)
    {
        if (headers == null) return;
        foreach (var header  in headers)
        {
            try
            {
              requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);  
            }
            catch (Exception ex)
            {
                
                 _logger.LogWarning(ex, $"Failed to add header: {header.Key}");;
            }
        }

    }
}