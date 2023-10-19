using Chat.Client.Library.Services;
using Chat.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextxUnitTraining.Module3.Moq.Tests.ChatFakes
{
    internal class UserApiClientFake : IUserApiClient
    {
        public Task<ChatUser> CreateUserAsync(string username, string password)
        {
            return Task.FromResult(new ChatUser()
            {
                IdUser = 1,
                Name = username,
                Password = password
            });
        }

        public Task<ChatUser> LoginAsync(string username, string password)
        {
            return Task.FromResult(new ChatUser()
            {
                IdUser = 1,
                Name = username,
                Password = password
            });
        }
    }
}
