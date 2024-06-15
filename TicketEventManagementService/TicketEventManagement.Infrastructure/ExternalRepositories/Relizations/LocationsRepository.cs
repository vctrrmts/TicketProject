using System.Net;
using System.Net.Http.Json;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;
using TicketEventManagement.Infrastructure.ExternalRepositories.Exceptions;

namespace TicketEventManagement.Infrastructure.ExternalRepositories.Relizations;

public class LocationsRepository : ILocationsRepository
{

    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public LocationsRepository(IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task CreateLocationAsync(Location location, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceLocationsApiUrl"];
        var createLocationApiMethodUrl = $"{ticketSearchServiceUrl}";
        JsonContent content = JsonContent.Create(location);
        var responseMessage = await _httpClient.PostAsync(createLocationApiMethodUrl, content, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{createLocationApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }

    public async Task DeleteLocationAsync(Guid locationId, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceLocationsApiUrl"];
        var deleteLocationApiMethodUrl = $"{ticketSearchServiceUrl}/{locationId}";
        var responseMessage = await _httpClient.DeleteAsync(deleteLocationApiMethodUrl, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{deleteLocationApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }

    public async Task UpdateLocationAsync(Location location, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceLocationsApiUrl"];
        var updateLocationApiMethodUrl = $"{ticketSearchServiceUrl}";
        JsonContent content = JsonContent.Create(location);
        var responseMessage = await _httpClient.PutAsync(updateLocationApiMethodUrl, content, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{updateLocationApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }
}