using Chat.Client.Library.Services;
using Chat.Common.Entities;
using Moq;
using System.Net;

namespace TestxUnitTraining.Module5.Advanced.Tests.ApiClientsTests
{
    [Trait("Module", "5")]
    public class ChatApiClientTests : IClassFixture<FakeHttpClientFixture>
    {
        private FakeMessageHandler _handler;
        private IChatApiClient _chatApiClient;
        public ChatApiClientTests(FakeHttpClientFixture fixture)
        {
            _handler = fixture.Handler;
            _chatApiClient = new ChatApiClient(fixture.HttpClient);
        }

        [Theory]
        [MemberData(nameof(SendMessageData))]
        public async Task SendMessage_ReturnsTrue_WithStatusCodeOK(ChatMessage message)
        {
            //Arrange
            // Configurar para que el httpclient devuelva un status code de OK. sin mensaje necesario
            _handler.SetExpectedResponse<ChatMessage>(HttpStatusCode.OK, null);

            // Act
            var sentOk = await _chatApiClient.SendMessageAsync(message);

            // Assert
            Assert.True(sentOk);
        }

        [Theory]
        [MemberData(nameof(SendMessageData))]
        public async Task SendMessage_ReturnsFalse_WithStatusCodeBadRequest(ChatMessage message)
        {
            //Arrange
            // Configurar para que el httpclient devuelva un status code de OK. sin mensaje necesario
            _handler.SetExpectedResponse<ChatMessage>(HttpStatusCode.BadRequest, null);

            // Act
            var sentOk = await _chatApiClient.SendMessageAsync(message);

            // Assert
            Assert.False(sentOk);
        }

        [Theory]
        [MemberData(nameof(GetChatMessages))]
        public async Task GetMessages_ReturnListMessages_WithStatusCodeOk(IEnumerable<ChatMessage> messages)
        {
            //Arrange
            // Configurar para que el httpclient devuelva un status code de OK. y con la lista de mensajes
            _handler.SetExpectedResponse(HttpStatusCode.OK, messages);

            // Act
            var receivedMessages = await _chatApiClient.GetChatMessagesAsync();

            // Assert
            Assert.Equivalent(messages, receivedMessages);
        }

        public static IEnumerable<object[]> SendMessageData
        {
            get
            {
                yield return new object[] { new ChatMessage() { Author = "User1", Date = DateTime.Now, Message = "Message 1" } };
            }
        }

        public static IEnumerable<object[]> GetChatMessages
        {
            get
            {
                List<ChatMessage> messages = new List<ChatMessage>();
                messages.Add(new ChatMessage() { Author = "User1", Date = DateTime.Now, Message = "Message 1" });
                messages.Add(new ChatMessage() { Author = "User2", Date = DateTime.Now, Message = "Message 2" });
                yield return new object[] { messages };
            }
        }
    }
}