using EventNotifyApp.ExternalProviders.Exceptions;
using System.Net;
using System.Text.Json;
using System.Configuration;
using Notification.Domain;

namespace EventNotifyApp.ExternalProviders
{
    public class TicketsProvider
    {
        private readonly HttpClient _httpClient;

        public TicketsProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BuyedTicket[]> GetBuyedTicketsByEventAsync(Guid eventId)
        {
            var ticketBuyingServiceUrl = ConfigurationManager.AppSettings["TicketBuyingServiceApiUrl"]!;
            var ticketBuyingApiMethodUrl = $"{ticketBuyingServiceUrl}/Tickets/EventId/{eventId}/Tickets";
            var responseMessage = await _httpClient.GetAsync(ticketBuyingApiMethodUrl);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "TicketBuyingService";
                var requestUrlMessage = $"request url '{ticketBuyingApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }

            var jsonResult = await responseMessage.Content.ReadAsStringAsync();
            var tickets = JsonSerializer.Deserialize<BuyedTicket[]>(jsonResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return tickets;
        }
    }
}
