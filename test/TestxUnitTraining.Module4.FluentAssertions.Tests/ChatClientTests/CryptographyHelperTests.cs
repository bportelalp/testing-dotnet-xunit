using Chat.Client.Library.Helpers;
using TestxUnitTraining.Module4.FluentAssertions.Tests.ChatClientTests.TestData;
using Xunit;

namespace TestxUnitTraining.Module4.FluentAssertions.Tests.ChatClientTests
{
    [Trait("Module", "3")]
    public class CryptographyHelperTests
    {
        [Theory]
        [ClassData(typeof(CryptographyHelperData))]
        public void CalculateHash_ShouldBeExpectedHash(string password, string expected)
        {
            //Arrange

            //Act
            var result = CryptographyHelper.CalculateHash(password);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
