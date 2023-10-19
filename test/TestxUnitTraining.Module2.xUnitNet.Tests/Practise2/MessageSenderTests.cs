using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using TestxUnitTraining.Module2.xUnitNet.Practise2.Entities;
using TestxUnitTraining.Module2.xUnitNet.Practise2.Services;
using TestxUnitTraining.Module2.xUnitNet.Tests.Practise2.Services;
using TestxUnitTraining.Tests.Common.Traits;
using Xunit;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Practise2
{
    [Trait("Module", "2")]
    [UseFixtureTrait(typeof(TcpServerFixture))]
    [Collection("TcpServer")]
    public class MessageSenderTests
    {
        private readonly IMessageSender _messageSender = null!;
        private readonly TcpServerFixture _server = null!;

        public MessageSenderTests(TcpServerFixture tcpServer)
        {
            _messageSender = new MessageSender(TcpServerFixture.Port, IPAddress.Loopback);
            _server = tcpServer;
            _server.Listen();
        }

        [Fact]
        public async Task SendMessageAsync_ShouldSendTheCorrectMessage()
        {
            //Arrange
            var messageData = new MessageData(DateTime.Now, new Author(1, "Pedro"), "TestMessage");

            //Act
            await _messageSender.SendMessageAsync(messageData);
            await Task.Delay(2000); //Damos tiempo a que la comunicación se establezca y recibamos el mensaje
            var message = _server.GetLastMessage<MessageData>();
            //Assert
            Assert.NotNull(message);
            Assert.Equal(messageData.Date, message.Date);
            Assert.Equal(messageData.Author.Id, message.Author.Id);
            Assert.Equal(messageData.Author.Name, message.Author.Name);
            Assert.Equal(messageData.Message, message.Message);
        }
    }
}
