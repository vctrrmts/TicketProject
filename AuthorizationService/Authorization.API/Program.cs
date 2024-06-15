using Authorization.API.Middlewares;
using Serilog.Events;
using Serilog;
using System.Text.Json.Serialization;
using Authorization.Application;
using Authorization.Infrastructure.Persistence;
using Authorization.Infrastructure.ExternalProviders;
using System.Reflection;

namespace Authorization.API;

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

            builder.Services
                .AddApiServices(Assembly.GetExecutingAssembly(), builder.Configuration)
                .AddPersistenceServices(builder.Configuration)
                .AddApplicationServices()
                .AddExternalProviders()
                .AddHttpClient();

            builder.Host.UseSerilog();
            var app = builder.Build();

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
