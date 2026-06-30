using HelloUltimateUtils.Utils;

var builder = Host.CreateApplicationBuilder(args);

builder
    .ConfigureServices()
    .Build()
    .Run();
