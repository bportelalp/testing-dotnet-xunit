using System;
using Chat.Client.Library.Helpers;
using Chat.Common.Entities;
using TestxUnitTraining.Module4.FluentAssertions.Tests.ChatClientTests.TestData;
using Xunit;

namespace TestxUnitTraining.Module4.FluentAssertions.Tests.ChatClientTests
{
    [Trait("Module", "3")]
    public class ChatMessageHelperTests
    {
        [Theory]
        [ClassData(typeof(ChatMessageHelperData))]
        public void GenerateString_ShouldBeExpectedMessage(ChatMessage message, string expected)
        {
            //Arrange

            //Act
            var result = ChatMessageHelper.GenerateString(message);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
