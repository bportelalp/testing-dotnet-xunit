using Chat.Client.Library.Services;
using Chat.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextxUnitTraining.Module3.Moq.Tests.ChatFakes
{
    internal class ChatApiClientFake : IChatApiClient
    {
        private List<ChatMessage> _fakeMessages = new();
        public Task<IEnumerable<ChatMessage>> GetChatMessagesAsync()
        {

            return Task.FromResult(_fakeMessages.AsEnumerable());
        }

        public Task<bool> SendMessageAsync(ChatMessage message)
        {
            _fakeMessages.Add(message);
            message.IdChatMessage = _fakeMessages.Count;
            return Task.FromResult(true);
        }

        /// <summary>
        /// Método usado para simular que entran mensajes nuevos en el servidor
        /// </summary>
        /// <param name="fakeMessages"></param>
        public void SetFakeMessages(IEnumerable<ChatMessage> fakeMessages)
        {
            foreach (ChatMessage message in fakeMessages)
            {
                _fakeMessages.Add(message);
                message.IdChatMessage = _fakeMessages.Count;
            }
        }
    }
}
