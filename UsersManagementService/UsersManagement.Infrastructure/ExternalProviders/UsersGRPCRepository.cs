using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using UsersManagement.Infrastructure.ExternalProviders.Exceptions;
using UsersManagement.Application.Abstractions.ExternalRepositories;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace UsersManagement.Infrastructure.ExternalProviders
{
    public class UsersGRPCRepository : IUsersGRPCRepository
    {
        private readonly IConfiguration _configuration;

        private readonly HttpClient _httpClient;

        public UsersGRPCRepository(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task CreateUserAsync(string userId, string login, string passwordHash, 
            CancellationToken cancellationToken)
        {
            var channel = GrpcChannel.ForAddress(_configuration["AuthorizationServiceGRPCUrl"]!);
            var client = new UsersService.UsersServiceClient(channel);

            try
            {
                await client.CreateUserAsync(new CreateUserRequest()
                {
                    UserId = userId,
                    Login = login,
                    PasswordHash = passwordHash
                }, cancellationToken: cancellationToken);
            }
            catch (Exception)
            {
                var serviceName = "Authorization Service";
                throw new ExternalServiceNotAvailable(serviceName, 
                    $"{_configuration["AuthorizationServiceGRPCUrl"]!}/{nameof(client.CreateUserAsync)}");
            }
        }

        public async Task DeleteUserAsync(string userId, CancellationToken cancellationToken)
        {
            var channel = GrpcChannel.ForAddress(_configuration["AuthorizationServiceGRPCUrl"]!);
            var client = new UsersService.UsersServiceClient(channel);

            try
            {
                await client.DeleteUserAsync(new DeleteUserRequest()
                {
                    UserId = userId
                }, cancellationToken: cancellationToken);
            }
            catch (Exception)
            {
                var serviceName = "Authorization Service";
                throw new ExternalServiceNotAvailable(serviceName,
                    $"{_configuration["AuthorizationServiceGRPCUrl"]!}/{nameof(client.DeleteUserAsync)}");
            }
        }

        public async Task UpdatePasswordAsync(string userId, string passwordHash, CancellationToken cancellationToken)
        {
            var channel = GrpcChannel.ForAddress(_configuration["AuthorizationServiceGRPCUrl"]!);
            var client = new UsersService.UsersServiceClient(channel);

            try
            {
                await client.UpdatePasswordAsync(new UpdatePasswordRequest()
                {
                    UserId = userId,
                    PasswordHash = passwordHash
                }, cancellationToken: cancellationToken);
            }
            catch (Exception)
            {
                var serviceName = "Authorization Service";
                throw new ExternalServiceNotAvailable(serviceName,
                    $"{_configuration["AuthorizationServiceGRPCUrl"]!}/{nameof(client.UpdatePasswordAsync)}");
            }
        }
    }
}
