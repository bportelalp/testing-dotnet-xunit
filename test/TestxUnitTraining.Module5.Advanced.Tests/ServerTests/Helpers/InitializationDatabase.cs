using Chat.Common.Entities;
using Chat.Server.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module5.Advanced.Tests.ServerTests.Helpers
{
    internal static class InitializationDatabase
    {
        private static readonly List<ChatMessage> _messages = new List<ChatMessage>()
        {
            new ChatMessage(){ IdChatMessage = 1, Author = "User1", Date = DateTime.Now, Message = "Message 1of User1" },
            new ChatMessage(){ IdChatMessage = 2, Author = "User2", Date = DateTime.Now, Message = "Message 1 of User2" },
            new ChatMessage(){ IdChatMessage = 3, Author = "User1", Date = DateTime.Now, Message = "Message 2 of User1" },
            new ChatMessage(){ IdChatMessage = 4, Author = "User2", Date = DateTime.Now, Message = "Message 2 of User2" },
        };

        private static readonly List<ChatUser> _users = new List<ChatUser>()
        {
            new ChatUser(){IdUser = 1, Name = "User1", Password="PassUser1"},
            new ChatUser(){IdUser = 2, Name = "User2", Password="PassUser2"}
        };

        public static void InitializeContext(ChatDbContext context)
        {
            context.ChatUsers.AddRange(_users);
            context.ChatMessages.AddRange(_messages);
            context.SaveChanges();
        }
    }
}
