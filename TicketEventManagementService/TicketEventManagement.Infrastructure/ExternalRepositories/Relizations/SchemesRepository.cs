using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;
using TicketEventManagement.Infrastructure.ExternalRepositories.Exceptions;

namespace TicketEventManagement.Infrastructure.ExternalRepositories.Relizations;

public class SchemesRepository : ISchemesRepository
{

    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public SchemesRepository(IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task CreateSchemeAsync(Scheme scheme, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceSchemesApiUrl"];
        var createSchemeApiMethodUrl = $"{ticketSearchServiceUrl}";
        JsonContent content = JsonContent.Create(scheme);
        var responseMessage = await _httpClient.PostAsync(createSchemeApiMethodUrl, content, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{createSchemeApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }

    public async Task UpdateSchemeAsync(Scheme scheme, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceSchemesApiUrl"];
        var createSchemeApiMethodUrl = $"{ticketSearchServiceUrl}";
        JsonContent content = JsonContent.Create(scheme);
        var responseMessage = await _httpClient.PutAsync(createSchemeApiMethodUrl, content, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{createSchemeApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }
}