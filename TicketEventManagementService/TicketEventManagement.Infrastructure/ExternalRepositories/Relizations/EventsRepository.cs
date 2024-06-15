using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;
using TicketEventManagement.Infrastructure.ExternalRepositories.Exceptions;

namespace TicketEventManagement.Infrastructure.ExternalRepositories.Relizations;

public class EventsRepository : IEventsRepository
{

    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public EventsRepository(IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task PublishEventAsync(EventForExportDto myEvent, string accessToken, 
        CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceEventsApiUrl"];
        var publishEventApiMethodUrl = $"{ticketSearchServiceUrl}";
        JsonContent content = JsonContent.Create(myEvent);
        var responseMessage = await _httpClient.PostAsync(publishEventApiMethodUrl, content, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{publishEventApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }

    public async Task UpdateEventAsync(EventForExportDto myEvent, string accessToken
        , CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceEventsApiUrl"];
        var updateEventApiMethodUrl = $"{ticketSearchServiceUrl}";
        JsonContent content = JsonContent.Create(myEvent);
        var responseMessage = await _httpClient.PutAsync(updateEventApiMethodUrl, content, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{updateEventApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }
}