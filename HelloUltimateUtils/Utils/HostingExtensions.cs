namespace HelloUltimateUtils.Utils;

internal static class HostingExtensions
{
    internal static HostApplicationBuilder ConfigureServices(this HostApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder
                .Services
                .AddLogging(
                    loggingBuilder =>
                    {
                        loggingBuilder.AddSeq();
                    });
        }

        builder.Services.AddHostedService<Worker>();

        return builder;
    }
}
