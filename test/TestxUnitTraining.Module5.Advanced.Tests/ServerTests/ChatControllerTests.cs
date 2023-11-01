using Chat.Common.Entities;
using Chat.Server.Library.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TestxUnitTraining.Module5.Advanced.Tests.ServerTests.Helpers;

namespace TestxUnitTraining.Module5.Advanced.Tests.ServerTests
{
    [Trait("Module", "5")]
    public class ChatControllerTests
    {
        private readonly TestServer _server;

        public ChatControllerTests()
        {
            var builder = new WebHostBuilder()
                .UseStartup<TestStartup>();
            _server = new TestServer(builder);
        }

        [Theory]
        [InlineData("User1","PassUser1")]
        public async Task GetChatUser_ShouldReturnUser(string userName, string password)
        {
            //Arrange
            var client = _server.CreateClient();

            //Act
            HttpResponseMessage response = await client.GetAsync($"api/chatUsers?username={userName}&password={password}");
            ChatUser? user = await response.Content.ReadFromJsonAsync<ChatUser>();
            //Assert
            user.Should().NotBeNull();
            user!.Name.Should().Be(userName);
            user.Password.Should().Be(password);
        }

        [Theory]
        [InlineData("User3", "PassUser3")]
        public async Task PostChatMessage_ShouldReturnMessageAndAllMessagesMustContainIt(string userName, string password)
        {
            //Arrange
            var client = _server.CreateClient();

            //Act
            HttpResponseMessage postResponse = await client.PostAsync($"api/chatUsers?username={userName}&password={password}", null);
            ChatUser? postedUser = await postResponse.Content.ReadFromJsonAsync<ChatUser>();

            HttpResponseMessage getResponse = await client.GetAsync($"api/chatUsers?username={userName}&password={password}");
            ChatUser? getUser = await getResponse.Content.ReadFromJsonAsync<ChatUser>();

            //Assert
            postedUser.Should().NotBeNull();
            postedUser!.Name.Should().Be(userName);
            postedUser.Password.Should().Be(password);

            getUser.Should().NotBeNull();
            getUser.Should().BeEquivalentTo(postedUser);
        }

        [Fact]
        public async Task GetChatMessages_ShouldReturnListOfMessages()
        {
            //Arrange
            var client = _server.CreateClient();

            //Act
            HttpResponseMessage response = await client.GetAsync($"api/chatMessages");
            IEnumerable<ChatMessage> listMessages = (await response.Content.ReadFromJsonAsync<IEnumerable<ChatMessage>>())!;
            //Assert
            listMessages.Should().NotBeNull();
            listMessages.Should().HaveCountGreaterThan(1);
        }

        [Theory]
        [InlineData("User1", "This is the new message")]
        public async Task PostChatMessage_ShouldReturnMessageAndGetSame(string userName, string message)
        {
            //Arrange
            var client = _server.CreateClient();
            var chatMessage = new ChatMessage() { Author = userName, Message = message, Date = DateTime.Now };
            //Act
            var content = new StringContent(JsonConvert.SerializeObject(chatMessage), new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            HttpResponseMessage postResponse = await client.PostAsync($"api/chatMessages", content);
            ChatMessage? postedMessage = await postResponse.Content.ReadFromJsonAsync<ChatMessage>();

            HttpResponseMessage getResponse = await client.GetAsync($"api/chatMessages");
            IEnumerable<ChatMessage> listMessages = (await getResponse.Content.ReadFromJsonAsync<IEnumerable<ChatMessage>>())!;

            //Assert
            postedMessage.Should().NotBeNull();
            // No totalmente equivalentes porque la respuesta viene con id
            postedMessage!.Author.Should().Be(userName);
            postedMessage.Message.Should().Be(message);
            postedMessage.IdChatMessage.Should().NotBe(0);

            listMessages.Should().NotBeNull();
            listMessages.Should().ContainEquivalentOf(postedMessage);
        }
    }
}
