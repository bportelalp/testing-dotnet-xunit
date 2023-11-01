using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Chat.Client.Library;
using Chat.Client.Library.Services;
using Chat.Common.Entities;
using Newtonsoft.Json;

namespace TestxUnitTraining.Module5.Advanced.Tests.ApiClientsTests
{
    [Trait("Module", "5")]
    public class UserApiClientTests : IClassFixture<FakeHttpClientFixture>
    {
        private FakeMessageHandler _handler;
        private IUserApiClient _userApiClient;
        public UserApiClientTests(FakeHttpClientFixture fixture)
        {
            _handler = fixture.Handler;
            _userApiClient = new UserApiClient(fixture.HttpClient);
        }

        [Theory]
        [InlineData("Usuario1", "CoNtrÄseñÁ")]
        [InlineData("Usuario2", "CoNtrÄseñÁ_2")]
        public async Task CreateUser_ReturnsEquivalentChatUser_WhenRegister(string user, string password)
        {
            //Arrange
            ChatUser chatUser = new ChatUser()
            {
                Name = user,
                Password = password
            };
            // Configurar para que el httpclient devuelva el propio usuario
            _handler.SetExpectedResponse(HttpStatusCode.OK, chatUser);

            // Act
            var chatUserRegistered = await _userApiClient.CreateUserAsync(user, password);

            // Assert
            Assert.Equal(user, chatUserRegistered.Name);
            Assert.Equal(password, chatUserRegistered.Password);
        }

        [Theory]
        [InlineData("Usuario1", "CoNtrÄseñÁ")]
        [InlineData("Usuario2", "CoNtrÄseñÁ_2")]
        public async Task LoginUser_ReturnsEquivalentChatUser_WhenRegister(string user, string password)
        {
            //Arrange
            ChatUser chatUser = new ChatUser()
            {
                Name = user,
                Password = password
            };
            // Configurar para que el httpclient devuelva el propio usuario
            _handler.SetExpectedResponse(HttpStatusCode.OK, chatUser);

            // Act
            var chatUserRegistered = await _userApiClient.LoginAsync(user, password);

            // Assert
            Assert.Equal(user, chatUserRegistered.Name);
            Assert.Equal(password, chatUserRegistered.Password);
        }
    }
}
