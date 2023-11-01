using Chat.Server.Library.Data;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestxUnitTraining.Module5.Advanced.Tests.ServerTests.Helpers;

namespace TestxUnitTraining.Module5.Advanced.Tests.ServerTests
{
    [Trait("Module", "5")]
    public class ChatDbContextTests : IDisposable
    {
        private readonly ChatDbContext _dbContext;
        private readonly SqliteConnection _connection;
        public ChatDbContextTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();


            var options = new DbContextOptionsBuilder<ChatDbContext>()
                .UseSqlite(_connection)
                .Options;

            _dbContext = new ChatDbContext(options);
            _dbContext.Database.EnsureCreated();
            InitializationDatabase.InitializeContext(_dbContext);
        }

        [Fact]
        public void User1_ShouldHaveTwoMessages()
        {
            var user1Messages = _dbContext.ChatMessages.Where(m => m.Author == "User1");

            user1Messages.Should().HaveCount(2);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            _connection?.Dispose();
        }
    }
}
