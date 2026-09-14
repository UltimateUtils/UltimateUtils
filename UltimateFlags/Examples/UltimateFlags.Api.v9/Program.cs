using UltimateFlags.Api.v9.Utils;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app =
    builder
        .ConfigureServices()
        .ConfigurePipeline();

app.Run();
