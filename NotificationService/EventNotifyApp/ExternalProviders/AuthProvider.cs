using EventNotifyApp.Models;
using System.Configuration;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using EventNotifyApp.ExternalProviders.Exceptions;

namespace EventNotifyApp.ExternalProviders;

public class AuthProvider
{
    private readonly HttpClient _httpClient;
    public AuthProvider(HttpClient httpClient) 
    {
        _httpClient = httpClient;
    }

    public async Task<string> CreateJwtTokenAsync(string login, string password)
    {
        var authServiceUrl = ConfigurationManager.AppSettings["AuthorizationServiceApiUrl"]!;
        var authApiMethodUrl = $"{authServiceUrl}/auth/CreateJwtToken";

        var command = new CreateTokenDto 
        { 
            Login = login, 
            Password = password!
        };
        JsonContent content = JsonContent.Create(command);
        var responseMessage = await _httpClient.PostAsync(authApiMethodUrl, content);

        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketManagementService";
            var requestUrlMessage = $"request url '{authApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }

        var jsonResult = await responseMessage.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<Notification.Domain.AccessToken>(jsonResult, 
            new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return token.JwtToken;
    }
}
