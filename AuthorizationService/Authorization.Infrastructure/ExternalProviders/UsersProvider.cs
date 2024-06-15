using System.Net;
using System.Net.Http;
using System.Text.Json;
using Authorization.Application.Abstractions.ExternalProviders;
using Authorization.Domain;
using Authorization.Infrastructure.ExternalProviders.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Authorization.Infrastructure.ExternalProviders;

public class UsersProvider : IUsersProvider
{

    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public UsersProvider(IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
    {
        var usersManagementServiceUrl = _configuration["UsersManagementServiceApiUrl"];
        var getUserByLoginApiMethodUrl = $"{usersManagementServiceUrl}/login?Login={login}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, getUserByLoginApiMethodUrl);
        var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "UsersManagementService";
            var requestUrlMessage = $"request url '{getUserByLoginApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }

        var jsonResult = await responseMessage.Content.ReadAsStringAsync(cancellationToken);

        var user = JsonSerializer.Deserialize<User>(jsonResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return user;
    }
}