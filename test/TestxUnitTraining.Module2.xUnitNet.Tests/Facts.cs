namespace TestxUnitTraining.Module2.xUnitNet.Tests
{
    /// <summary>
    /// Usar el atributo <see cref="FactAttribute"/> para decorar métodos que realicen una única llamada
    /// de prueba sin parámetros
    /// </summary>
    [Trait("Module", "2")]
    public class Facts
    {
        [Fact]
        public void Sum_ShouldBe5_Ifx3Andy2()
        {
            // Arrange
            var x = 2;
            var y = 3;

            // Act
            var result = EasyCalculator.Sum(x, y);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Divide_ShouldBe5_Ifx50AndY10()
        {
            // Arrange
            var x = 50;
            var y = 10;

            // Act
            var result = EasyCalculator.Divide(x, y);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Divide_ShouldThrowEx_IfDivideZero()
        {
            // Arrange
            var x = 50;
            var y = 0;

            // Act. void is not called until Assert
            void act() => EasyCalculator.Divide(x, y);

            // Assert
            Assert.Throws<DivideByZeroException>(act);
        }
    }
}