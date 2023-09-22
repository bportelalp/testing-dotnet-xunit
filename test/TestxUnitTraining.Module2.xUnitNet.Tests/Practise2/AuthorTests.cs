using System;
using TestxUnitTraining.Module2.xUnitNet.Practise2.Entities;
using Xunit;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Practise2
{
    [Trait("Category", "Module2")]
    public class AuthorTests
    {
        [Theory]
        [InlineData(1, "Pablo Neruda")]
        [InlineData(2, "Camilo Jose Cela")]
        [InlineData(3, "Miguel de Cervantes")]
        public void Contructor_ShouldCreateCorrectProperties(int id, string name)
        {
            //Arrange

            //Act
            var author = new Author(id, name);

            //Assert
            Assert.Equal(id, author.Id);
            Assert.Equal(name, author.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        public void Contructor_ShouldCreateThrowArgumentNullException_IfIdIs0OrLess(int id)
        {
            //Arrange
            var name = "Jorge";

            //Act
            Action act = () =>
            {
                _ = new Author(id, name);
            };

            //Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Contructor_ShouldCreateThrowArgumentNullException_IfNAmeIsNullOrWhiteSpace(string name)
        {
            //Arrange
            var id = 1;

            //Act
            Action act = () =>
            {
                _ = new Author(id, name);
            };

            //Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
