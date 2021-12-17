namespace OrderActivityService.Handlers
{
    using Azure.Messaging.ServiceBus;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class OrderActivityHandler
    {
        private readonly ILogger _logger;
        private readonly ServiceBusClient _client;
        private ServiceBusSender _sender;

        public OrderActivityHandler(ILogger<OrderActivityHandler> logger, ServiceBusClient client)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task SendMessage(string message, string queueOrTopicName)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            _sender = _client.CreateSender(queueOrTopicName);

            try
            {
                await _sender.SendMessageAsync(new ServiceBusMessage(message));
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong trying to process the message: {e.Message}");
            }
        }
    }
}
