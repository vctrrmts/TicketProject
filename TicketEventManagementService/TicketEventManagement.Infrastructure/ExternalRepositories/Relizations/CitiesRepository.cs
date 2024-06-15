using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;
using TicketEventManagement.Infrastructure.ExternalRepositories.Exceptions;

namespace TicketEventManagement.Infrastructure.ExternalRepositories.Relizations;

public class CitiesRepository : ICitiesRepository
{

    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public CitiesRepository(IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task CreateCityAsync(City city, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceCitiesApiUrl"];
        var createCityApiMethodUrl = $"{ticketSearchServiceUrl}";
        JsonContent content = JsonContent.Create(city);
        var responseMessage = await _httpClient.PostAsync(createCityApiMethodUrl, content, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{createCityApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }

    public async Task DeleteCityAsync(Guid cityId, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceCitiesApiUrl"];
        var createCityApiMethodUrl = $"{ticketSearchServiceUrl}/{cityId}";
        var responseMessage = await _httpClient.DeleteAsync(createCityApiMethodUrl, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{createCityApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }

    public async Task UpdateCityAsync(City city, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceCitiesApiUrl"];
        var updateCityApiMethodUrl = $"{ticketSearchServiceUrl}";
        JsonContent content = JsonContent.Create(city);
        var responseMessage = await _httpClient.PutAsync(updateCityApiMethodUrl, content, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{updateCityApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }
}