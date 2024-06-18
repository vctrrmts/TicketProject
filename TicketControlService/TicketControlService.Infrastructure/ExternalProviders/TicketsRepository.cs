using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TicketControlService.Application.Abstractions.ExternalProviders;
using TicketControlService.Domain;
using TicketControlService.Infrastructure.ExternalProviders.Exceptions;

namespace TicketControlService.Infrastructure.ExternalProviders;

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

    public async Task<Ticket> GetTicketAsync(string hashFromQR, CancellationToken cancellationToken)
    {
        var ticketServiceUrl = _configuration["TicketBuyingServiceApiUrl"];
        var ticketBuyingApiMethodUrl = $"{ticketServiceUrl}/Tickets/Verify";
        var command = new VerifyCommand { HashGuid = hashFromQR };
        JsonContent content = JsonContent.Create(command);
        var responseMessage = await _httpClient.PostAsync(ticketBuyingApiMethodUrl, content, cancellationToken);

        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketManagementService";
            var requestUrlMessage = $"request url '{ticketBuyingApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }

        var jsonResult = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        var ticket = JsonSerializer.Deserialize<Ticket>(jsonResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return ticket;
    }
}