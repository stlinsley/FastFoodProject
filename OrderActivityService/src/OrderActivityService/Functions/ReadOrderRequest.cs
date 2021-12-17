namespace OrderActivityService
{
    using Azure.Messaging.ServiceBus;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using OrderActivityService.Handlers;
    using System;
    using System.Threading.Tasks;

    public class ReadOrderRequest
    {
        private readonly ServiceBusClient _client;
        private readonly ILogger<OrderActivityHandler> _logger;

        public ReadOrderRequest(ServiceBusClient client, ILogger<OrderActivityHandler> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName("ReadOrderRequest")]
        public async Task RunAsync([ServiceBusTrigger("sbt-orderactivity-raw", "sbs-orderactivity-raw", Connection = "sb-orderactivity-connectionstring")] string message)
        {
            var handler = new OrderActivityHandler(_logger, _client);
            await handler.SendMessage(message, "sbq-orderactivities-decorated");
            await handler.SendMessage(message, "sbt-orderactivity-submitted");
        }
    }
}
