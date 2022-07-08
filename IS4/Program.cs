using IS4;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

try
{
    IConfiguration configuration = GetConfiguration();
    ConfigureLogger(configuration);

    Log.Information("Getting Free-IS4 motors running... ;/");

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.GetServicesConfiguration(configuration);
    builder.Host.GetServicesConfiguration();

    var app = builder.Build();
    app.GetAppBuilderConfiguration(app.Environment);

    Log.Information("Successfully configured services... ;/");
    Log.Information("Successfully started Free-IS4 web application ;)");

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Free-IS4 host terminated unexpectedly... ;(");
}
finally
{
    Log.Information("Free-IS4 shut down complete... ;/");
    Log.CloseAndFlush();
}

static IConfiguration GetConfiguration()
{
    return
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets<Program>(optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
}

static void ConfigureLogger(IConfiguration configuration)
{
    Log.Logger =
        new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.ApplicationInsights(
            new TelemetryConfiguration { InstrumentationKey = configuration["APPLICATION_INSIGHTS:INSTRUMENTATION_KEY"] },
                TelemetryConverter.Events
            ).CreateBootstrapLogger();
}