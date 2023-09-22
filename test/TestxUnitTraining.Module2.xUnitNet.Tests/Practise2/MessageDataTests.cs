using System;
using TestxUnitTraining.Module2.xUnitNet.Practise2.Entities;
using Xunit;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Practise2
{
    [Trait("Category", "Module2")]
    public class MessageDataTests
    {

        [Theory]
        [InlineData(1, "Pablo Neruda", "Hola amigo")]
        [InlineData(2, "Camilo Jose Cela", "Buenos días")]
        [InlineData(3, "Miguel de Cervantes", "Bienvenido")]
        public void Contructor_ShouldCreateCorrectProperties(int id, string name, string message)
        {
            //Arrange
            var date = DateTime.Now;
            var author = new Author(id, name);

            //Act
            var messageData = new MessageData(date, author, message);

            //Assert
            Assert.Equal(id, messageData.Author.Id);
            Assert.Equal(name, messageData.Author.Name);
            Assert.Equal(date, messageData.Date);
            Assert.Equal(message, messageData.Message);
        }

        [Fact]
        public void Contructor_ShouldCreateThrowArgumentNullException_AuthorIsNull()
        {
            //Arrange
            var date = DateTime.Now;
            var message = "Message";

            //Act
            Action act = () =>
            {
                _ = new MessageData(date, null!, message);
            };

            //Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Contructor_ShouldCreateThrowArgumentNullException_IfMessageIsNullOrWhiteSpace(string message)
        {
            //Arrange
            var date = DateTime.Now;
            var author = new Author(1, "Jorge");

            //Act
            Action act = () =>
            {
                _ = new MessageData(date, author, message);
            };

            //Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
