using UltimateFlags.Api.v8.Utils;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app =
    builder
        .ConfigureServices()
        .ConfigurePipeline();

app.Run();
