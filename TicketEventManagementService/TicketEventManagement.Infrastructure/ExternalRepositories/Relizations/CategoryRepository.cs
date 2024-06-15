using Azure.Core;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;
using TicketEventManagement.Infrastructure.ExternalRepositories.Exceptions;

namespace TicketEventManagement.Infrastructure.ExternalRepositories.Relizations;

public class CategoryRepository : ICategoryRepository
{
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public CategoryRepository(IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task CreateCategoryAsync(Category category, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceCategoriesApiUrl"];
        var createCategoryApiMethodUrl = $"{ticketSearchServiceUrl}";
        JsonContent content = JsonContent.Create(category);
        var responseMessage = await _httpClient.PostAsync(createCategoryApiMethodUrl, content, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{createCategoryApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }

    public async Task UpdateCategoryAsync(Category category, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceCategoriesApiUrl"];
        var updateCategoryApiMethodUrl = $"{ticketSearchServiceUrl}";
        JsonContent content = JsonContent.Create(category);
        var responseMessage = await _httpClient.PutAsync(updateCategoryApiMethodUrl, content, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{updateCategoryApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }

    public async Task DeleteCategoryAsync(Guid categoryId, string accessToken, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var ticketSearchServiceUrl = _configuration["SearchServiceCategoriesApiUrl"];
        var createCategoryApiMethodUrl = $"{ticketSearchServiceUrl}/{categoryId}";
        var responseMessage = await _httpClient.DeleteAsync(createCategoryApiMethodUrl, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "TicketSearchService";
            var requestUrlMessage = $"request url '{createCategoryApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }
    }
}
