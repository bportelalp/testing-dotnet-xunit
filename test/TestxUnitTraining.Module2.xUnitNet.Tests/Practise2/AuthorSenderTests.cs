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
    [Trait("Category", "Module2")]
    [UseFixtureTrait(typeof(TcpServerFixture))]
    [Collection("TcpServer")]
    public class AuthorSenderTests
    {
        private readonly TcpServerFixture _server;
        private readonly IAuthorSender _authorSender;
        public AuthorSenderTests(TcpServerFixture server)
        {
            _server = server; 
            _server.Listen();
            _authorSender = new AuthorSender(TcpServerFixture.Port, IPAddress.Loopback);
        }

        [Fact]
        public async Task SendMessageAsync_ShouldSendTheCorrectMessage()
        {
            //Arrange
            var author = new Author(2, "Kant");

            //Act
            await Task.Delay(3000);
            await _authorSender.SendAuthorAsync(author);
            await Task.Delay(200); //Damos tiempo a que la comunicación se establezca y recibamos el mensaje
            var received = _server.GetLastMessage<Author>();
            //Assert
            Assert.NotNull(_authorSender);
            Assert.NotNull(received);
            Assert.Equal(author.Id, received.Id);
            Assert.Equal(author.Name, received.Name);
        }
    }
}
