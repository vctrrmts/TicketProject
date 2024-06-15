using Azure.Core;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Infrastructure.ExternalRepositories.Exceptions;

namespace TicketEventManagement.Infrastructure.ExternalRepositories.Relizations
{
    public class SeatsRepository : ISeatsRepository
    {
        private readonly IConfiguration _configuration;

        private readonly HttpClient _httpClient;

        public SeatsRepository(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task AddRangeOfSeatsAsync(SeatForExportDto[] seats, string accessToken
            , CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
            var ticketSearchServiceUrl = _configuration["SearchServiceSeatsApiUrl"];
            var AddRangeOfSeatsApiMethodUrl = $"{ticketSearchServiceUrl}/AddSeats";
            JsonContent content = JsonContent.Create(seats);
            string s = await content.ReadAsStringAsync();
            var responseMessage = await _httpClient.PostAsync(AddRangeOfSeatsApiMethodUrl, content, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "TicketSearchService";
                var requestUrlMessage = $"request url '{AddRangeOfSeatsApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task RemoveRangeOfSeatsAsync(SeatForExportDto[] seats, string accessToken
            , CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
            var ticketSearchServiceUrl = _configuration["SearchServiceSeatsApiUrl"];
            var RemoveRangeOfSeatsApiMethodUrl = $"{ticketSearchServiceUrl}/RemoveSeats";
            JsonContent content = JsonContent.Create(seats);
            var responseMessage = await _httpClient.PostAsync(RemoveRangeOfSeatsApiMethodUrl, content, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "TicketSearchService";
                var requestUrlMessage = $"request url '{RemoveRangeOfSeatsApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }
    }
}
