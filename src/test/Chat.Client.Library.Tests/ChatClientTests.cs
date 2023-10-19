using Chat.Client.Library.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Common.Entities;
using Xunit;

namespace Chat.Client.Library.Tests
{
    public class ChatClientTests : IDisposable
    {
        private readonly IChatClient _chatClient;

        public ChatClientTests()
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
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LoginAsync_ShouldBeArgumentNullException_IfArgumentNull()
        {
            //Arrange

            //Act
            Func<Task> act = async () => { _ = await _chatClient.LoginAsync(null, null); };

            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(act);

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
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CreateUserAsync_ShouldBeArgumentNullException_IfArgumentNull()
        {
            //Arrange

            //Act
            Func<Task> act = async () => { _ = await _chatClient.CreateUserAsync(null, null); };

            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(act);

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
            Assert.True(result);
        }

        [Fact]
        public async Task SendMessageAsync_ShouldBeFalse_IfNotLogged()
        {
            //Arrange
            var message = "Message1";

            //Act
            var result = await _chatClient.SendMessageAsync(message);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task NewMessageRecived_ShouldBeRaised_IfConnect()
        {
            var eventRecived = false;
            await _chatClient.LoginAsync("Usuario1", "P2ssw0rd!");
            _chatClient.NewMessageRecived += (sender, _) => eventRecived = true;
            _chatClient.Connect();

            //Act
            await Task.Delay(100); //Le damos tiempo para que se ejecute el evento

            //Assert
            Assert.True(eventRecived);
        }

        [Fact]
        public async Task OverwriteLastLine_ShouldBeRaised_IfOneMessageAndAuthorIsWhoseIsLogged()
        {
            var eventRecived = false;
            await _chatClient.LoginAsync("Usuario3", "P2ssw0rd!");
            _chatClient.OverwriteLastLine += (sender, _) => eventRecived = true;
            _chatClient.Connect();

            //Act
            await Task.Delay(100); //Le damos tiempo para que se ejecute el evento

            //Assert
            Assert.True(eventRecived);
        }

        [Fact]
        public async Task OverwriteLastLine_ShouldNotBeRaised_IfOneMessageAndAuthorIsOther()
        {
            var eventRecived = false;
            await _chatClient.LoginAsync("Usuario1", "P2ssw0rd!");
            _chatClient.OverwriteLastLine += (sender, _) => eventRecived = true;
            _chatClient.Connect();

            //Act
            await Task.Delay(100); //Le damos tiempo para que se ejecute el evento

            //Assert
            Assert.False(eventRecived);
        }

        [Fact]
        public async Task OverwriteLastLine_ShouldNotBeRaised_IfMoreThanOneMessage()
        {
            var eventRecived = false;
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

            await _chatClient.LoginAsync("Usuario1", "P2ssw0rd!");
            _chatClient.OverwriteLastLine += (sender, _) => eventRecived = true;
            _chatClient.Connect();

            //Act
            await Task.Delay(100); //Le damos tiempo para que se ejecute el evento

            //Assert
            Assert.False(eventRecived);
        }

        public void Dispose()
        {
            _chatClient?.Dispose();
        }
    }
}
