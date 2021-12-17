using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(OrderActivityService.Startup))]
namespace OrderActivityService
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            builder.Services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(configuration["sb-orderactivity-connectionstring"]);
            });

            builder.Services.AddLogging();
        }
    }   
}
