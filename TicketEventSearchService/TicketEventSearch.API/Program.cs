using Serilog.Events;
using Serilog;
using System.Text.Json.Serialization;
using TicketEventSearch.API.Middlewares;
using TicketEventSearch.Application;
using TicketEventSearch.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using TicketEventSearch.Infrastructure.DistributedCache;
using System.Reflection;

namespace TicketEventSearch.API;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .WriteTo.File("Logs/Information-.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
            .WriteTo.File("Logs/Error-.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services
                .AddApiServices(Assembly.GetExecutingAssembly(),builder.Configuration)
                .AddPersistenceServices(builder.Configuration)
                .AddApplicationServices()
                .AddHttpClient()
                .AddDistributedCacheServices(builder.Configuration);

            var app = builder.Build();

            app.RunDbMigrations();

            app.UseCoreExceptionHandler()
                .UseAuthExceptionHandler()
                .UseSwagger()
                .UseSwaggerUI()
                .UseAuthentication()
                .UseAuthorization()
                .UseHttpsRedirection()
                .UseCors();
            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Run Error");
            throw;
        }

    }
}
