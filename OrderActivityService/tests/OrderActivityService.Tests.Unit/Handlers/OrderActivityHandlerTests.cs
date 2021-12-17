namespace OrderActivityService.Tests.Unit.Handlers
{
    using Azure.Messaging.ServiceBus;
    using FluentAssertions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using OrderActivityService.Handlers;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class OrderActivityHandlerTests
    {
        private readonly Mock<ILogger<OrderActivityHandler>> _logger;
        private readonly Mock<ServiceBusClient> _client;
        private readonly Mock<ServiceBusSender> _sender;

        public OrderActivityHandlerTests()
        {
            _logger = new Mock<ILogger<OrderActivityHandler>>(MockBehavior.Strict);
            _logger.Setup(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
            _client = new Mock<ServiceBusClient>(MockBehavior.Strict);
            _sender = new Mock<ServiceBusSender>(MockBehavior.Strict);
            _client.Setup(x => x.CreateSender(It.IsAny<string>())).Returns(_sender.Object);
        }

        [Fact]
        public void OrderActivitiesHandler_LoggerNull_ThrowsArgumentNullException()
        {
            //arrange
            Action sut = () => new OrderActivityHandler(null, _client.Object);

            //act & assert
            sut.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void OrderActivitiesHandler_ServiceBusClientNull_ThrowsArgumentNullException()
        {
            //arrange
            Action sut = () => new OrderActivityHandler(_logger.Object, null);

            //act & assert
            sut.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task MessageHandler_OrderActivitiesDecoratedHandlerThrowsException_LogsError()
        {
            //arrange
            var handler = new OrderActivityHandler(_logger.Object, _client.Object);
            _sender.Setup(x => x.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            //act
            await handler.SendMessage("new message", "some queue");

            //assert
            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()));
        }

        [Fact]
        public async Task MessageHandler_WorksFine()
        {
            //arrange
            var handler = new OrderActivityHandler(_logger.Object, _client.Object);

            //act & assert
            await handler.SendMessage("my message", "some queue");
        }
    }
}
