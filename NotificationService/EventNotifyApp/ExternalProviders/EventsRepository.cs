using System.Configuration;
using System.Net;
using System.Text.Json;
using Notification.Domain;
using EventNotifyApp.ExternalProviders.Exceptions;
using System.Net.Http.Json;

namespace EventNotifyApp.ExternalProviders
{
    public class EventsRepository
    {
        private readonly HttpClient _httpClient;

        public EventsRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Event[]> GetEventsFromSearchServiceAsync()
        {
            var searchServiceUrl = ConfigurationManager.AppSettings["SearchServiceApiUrl"]!;
            var searchServiceApiMethodUrl = $"{searchServiceUrl}/Events";
            var responseMessage = await _httpClient.GetAsync(searchServiceApiMethodUrl);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "TicketEventSearchService";
                var requestUrlMessage = $"request url '{searchServiceApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }

            var jsonResult = await responseMessage.Content.ReadAsStringAsync();
            var events = JsonSerializer.Deserialize<Event[]>(jsonResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return events;
        }

        public async Task<NotifiedEvent[]> GetNotifiedEventsAsync()
        {
            var notificationServiceUrl = ConfigurationManager.AppSettings["NotificationServiceApiUrl"]!;
            var notiServiceApiMethodUrl = $"{notificationServiceUrl}/Events";
            var responseMessage = await _httpClient.GetAsync(notiServiceApiMethodUrl);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "NotificationService";
                var requestUrlMessage = $"request url '{notiServiceApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }

            var jsonResult = await responseMessage.Content.ReadAsStringAsync();
            var events = JsonSerializer.Deserialize<NotifiedEvent[]>(jsonResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return events;
        }

        public async Task AddNotifiedEventAsync(Guid eventId)
        {
            var notificationServiceUrl = ConfigurationManager.AppSettings["NotificationServiceApiUrl"]!;
            var notiServiceApiMethodUrl = $"{notificationServiceUrl}/Events";
            var command = new { EventId = eventId};
            JsonContent content = JsonContent.Create(command);
            var responseMessage = await _httpClient.PostAsync(notiServiceApiMethodUrl, content);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "NotificationService";
                var requestUrlMessage = $"request url '{notiServiceApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }
    }
}
