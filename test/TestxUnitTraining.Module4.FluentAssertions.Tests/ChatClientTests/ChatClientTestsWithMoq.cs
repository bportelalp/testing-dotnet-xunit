using Chat.Client.Library.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Common.Entities;
using Xunit;
using Moq;
using System.Linq;

namespace TestxUnitTraining.Module4.FluentAssertions.Tests.ChatClientTests
{
    [Trait("Module", "4")]
    [Trait("ObjectSim", "Moq")]
    [Trait("Category","FluentAssertions")]
    public class ChatClientTestsWithMoq : IDisposable
    {
        private readonly IChatClient _chatClient;
        private readonly IChatApiClient _chatApiClient;
        private readonly IUserApiClient _userApiClient;

        public ChatClientTestsWithMoq()
        {
            var messages = new List<ChatMessage>
            {
                new ChatMessage
                {
                    Author = "Usuario3",
                    Message = "Test",
                    Date = DateTime.Now
                }
            };
            //Creacion y configuracion de Mocks y creacion de IchatClient

            var chatApiMoq = Mock.Of<IChatApiClient>();
            /*
            Mock ChatApiClient:
            - Cuando se envia un mensaje se usa el callback para ir guardando los mensajes previos que se han enviado. Se asume que
                el mensaje siempre se envía de forma correcta
            - Cuando se obtienen los mensajes se devuelve esa lista siempre.
             */
            Mock.Get(chatApiMoq)
                .Setup(api => api.SendMessageAsync(It.IsAny<ChatMessage>()))
                .Callback((ChatMessage msg) => messages.Add(msg))
                .Returns(Task.FromResult(true));

            Mock.Get(chatApiMoq)
                .Setup(api => api.GetChatMessagesAsync())
                .Returns(Task.FromResult(messages.AsEnumerable()));



            Func<string, string, Task<ChatUser>> userApiReturns = (user, pass) => Task.FromResult(new ChatUser() { IdUser = 1, Name = user, Password = pass });
            var userApiMoq = Mock.Of<IUserApiClient>();
            Mock.Get(userApiMoq)
                .Setup(us => us.CreateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(userApiReturns);
            Mock.Get(userApiMoq)
                .Setup(us => us.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(userApiReturns);

            _chatApiClient = chatApiMoq;
            _userApiClient = userApiMoq;
            _chatClient = new ChatClient(chatApiMoq, userApiMoq);
        }

        [Theory]
        [InlineData(true, "Usuario1", "P2ssw0rd!")]
        [InlineData(false, "Usuario2", "")]
        public async Task LoginAsync_ShouldBeExpectedResult(bool expected, string username, string password)
        {
            //Arrange

            //Act
            var result = await _chatClient.LoginAsync(username, password);

            //Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void LoginAsync_ShouldBeArgumentNullException_IfArgumentNull()
        {
            //Arrange

            //Act
            Func<Task> act = async () => { _ = await _chatClient.LoginAsync(null, null); };

            //Assert
            act.Should().ThrowAsync<ArgumentNullException>();

        }

        [Theory]
        [InlineData(true, "Usuario1", "P2ssw0rd!")]
        [InlineData(false, "Usuario2", "")]
        public async Task CreateUserAsync_ShouldBeExpectedResult(bool expected, string username, string password)
        {
            //Arrange

            //Act
            var result = await _chatClient.CreateUserAsync(username, password);

            //Assert
            result.Should().Be(expected);
            Mock.Get(_userApiClient)
                .Verify(u => u.CreateUserAsync(It.IsAny<string>(), It.IsAny<string>()), expected ? Times.Once() : Times.Never());
        }

        [Fact]
        public void CreateUserAsync_ShouldBeArgumentNullException_IfArgumentNull()
        {
            //Arrange
            //Act
            Func<Task> act = async () => { _ = await _chatClient.CreateUserAsync(null, null); };

            //Assert
            act.Should().ThrowAsync<ArgumentNullException>();
            // El Mock no pudo ser llamado porque los argumentos se deben comprobar en el cliente, no en la api
            Mock.Get(_userApiClient)
                .Verify(u => u.CreateUserAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

        }

        [Fact]
        public async Task SendMessageAsync_ShouldBeTrue_IfLoggedAndConnected()
        {
            //Arrange
            var message = "Message1";
            await _chatClient.LoginAsync("Usuario1", "P2ssw0rd!");
            _chatClient.Connect();

            //Act
            var result = await _chatClient.SendMessageAsync(message);

            //Assert
            result.Should().BeTrue();
            Mock.Get(_userApiClient)
                .Verify(u => u.LoginAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Mock.Get(_chatApiClient)
                .Verify(u => u.SendMessageAsync(It.IsAny<ChatMessage>()), Times.Once());
        }

        [Fact]
        public async Task SendMessageAsync_ShouldBeFalse_IfNotLogged()
        {
            //Arrange
            var message = "Message1";

            //Act
            var result = await _chatClient.SendMessageAsync(message);

            //Assert
            result.Should().BeFalse();
            Mock.Get(_chatApiClient)
                .Verify(u => u.SendMessageAsync(It.IsAny<ChatMessage>()), Times.Never());
        }

        [Fact]
        public async Task NewMessageRecived_ShouldBeRaised_IfConnect()
        {
            using var monitoredSubject = _chatClient.Monitor();

            await _chatClient.LoginAsync("Usuario1", "P2ssw0rd!");
            _chatClient.NewMessageRecived += (_, _) => { };
            _chatClient.Connect();

            //Act
            await Task.Delay(100); //Le damos tiempo para que se ejecute el evento

            //Assert
            monitoredSubject.Should().Raise(nameof(IChatClient.NewMessageRecived))
                .WithSender(_chatClient);
        }

        [Fact]
        public async Task OverwriteLastLine_ShouldBeRaised_IfOneMessageAndAuthorIsWhoseIsLogged()
        {
            using var monitoredSubject = _chatClient.Monitor();
            await _chatClient.LoginAsync("Usuario3", "P2ssw0rd!");
            _chatClient.OverwriteLastLine += (_, _) => { };
            _chatClient.Connect();

            //Act
            await Task.Delay(100); //Le damos tiempo para que se ejecute el evento

            //Assert
            monitoredSubject.Should().Raise(nameof(IChatClient.OverwriteLastLine))
                .WithSender(_chatClient);
        }

        [Fact]
        public async Task OverwriteLastLine_ShouldNotBeRaised_IfOneMessageAndAuthorIsOther()
        {
            using var monitoredSubject = _chatClient.Monitor();
            await _chatClient.LoginAsync("Usuario1", "P2ssw0rd!");
            _chatClient.OverwriteLastLine += (_, _) => { };
            _chatClient.Connect();

            //Act
            await Task.Delay(100); //Le damos tiempo para que se ejecute el evento

            //Assert
            monitoredSubject.Should().NotRaise(nameof(IChatClient.OverwriteLastLine));
        }

        [Fact]
        public async Task OverwriteLastLine_ShouldNotBeRaised_IfMoreThanOneMessage()
        {
            using var monitoredSubject = _chatClient.Monitor();
            var messages = new List<ChatMessage>
            {
                new ChatMessage
                {
                    Author = "Usuario3",
                    Message = "Test",
                    Date = DateTime.Now
                },
                new ChatMessage
                {
                    Author = "Usuario3",
                    Message = "Test",
                    Date = DateTime.Now
                }
            };
            //Modificacion de Mock
            foreach (var msg in messages)
            {
                await _chatApiClient.SendMessageAsync(msg);
            }

            await _chatClient.LoginAsync("Usuario3", "P2ssw0rd!");
            _chatClient.OverwriteLastLine += (_, _) => { };
            _chatClient.Connect();

            //Act
            await Task.Delay(100); //Le damos tiempo para que se ejecute el evento

            //Assert
            monitoredSubject.Should().NotRaise(nameof(IChatClient.OverwriteLastLine));
        }

        public void Dispose()
        {
            _chatClient?.Dispose();
        }
    }
}
