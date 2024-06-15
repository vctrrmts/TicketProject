using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TicketBuying.Application.Abstractions.ExternalProviders;
using TicketBuying.Domain;
using TicketBuying.Infrastructure.ExternalProviders.Exceptions;

namespace TicketBuying.Infrastructure.ExternalProviders;

public class TicketsRepository : ITicketsRepository
{

    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public TicketsRepository(IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<Ticket> UpdateTicketStatusAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        var ticketServiceUrl = _configuration["TicketSearchServiceApiUrl"];
        var ticketManagementApiMethodUrl = $"{ticketServiceUrl}/Status";
        var command = new { TicketId = ticketId, TicketStatusId = 3 };
        JsonContent content = JsonContent.Create(command);
        var responseMessage = await _httpClient.PutAsync(ticketManagementApiMethodUrl, content, cancellationToken);

        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketManagementService";
            var requestUrlMessage = $"request url '{ticketManagementApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }

        var jsonResult = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        var ticketDto = JsonSerializer.Deserialize<Ticket>(jsonResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return ticketDto;
    }
}