using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using TestxUnitTraining.Module2.xUnitNet.Practise2.Entities;
using TestxUnitTraining.Module2.xUnitNet.Practise2.Services;
using TestxUnitTraining.Module2.xUnitNet.Tests.Practise2.Services;
using Xunit;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Practise2
{
    [Trait("Category", "Module2")]
    public class AuthorSenderTests : IDisposable
    {
        private const int Port = 43256;
        private readonly IAuthorSender _authorSender;
        private readonly TcpServer _tcpServer;
        private Author _authorRecived = null!;
        public AuthorSenderTests()
        {
            _tcpServer = new TcpServer();
            _tcpServer.Escuchar(Port);
            _tcpServer.DataReceived += (message) => _authorRecived = JsonConvert.DeserializeObject<Author>(message)!;
            _authorSender = new AuthorSender(Port, IPAddress.Loopback);
        }

        [Fact]
        public async Task SendMessageAsync_ShouldSendTheCorrectMessage()
        {
            //Arrange
            var author = new Author(2, "Kant");

            //Act
            await _authorSender.SendAuthorAsync(author);
            await Task.Delay(200); //Damos tiempo a que la comunicación se establezca y recibamos el mensaje

            //Assert
            Assert.NotNull(_authorSender);
            Assert.Equal(author.Id, _authorRecived.Id);
            Assert.Equal(author.Name, _authorRecived.Name);
        }

        public void Dispose()
        {
            _tcpServer.Desconectar();
        }
    }
}
