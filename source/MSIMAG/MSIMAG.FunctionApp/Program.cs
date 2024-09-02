using Locwise.Fireberry.Client;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSIMAG.Core;
using MSIMAG.Core.Data;


IFireberryClientFactory factory = new FireberryClientFactory();

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddTransient<IFireberryClient>(x => factory.Create(Consts.API_KEY, true));
        services.AddTransient<IDataManager, DataManager>();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
