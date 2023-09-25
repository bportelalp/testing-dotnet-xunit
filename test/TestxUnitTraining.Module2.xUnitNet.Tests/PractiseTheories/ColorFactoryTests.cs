using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.PractiseTheories
{
    /// <summary>
    ///
    /// </summary>
    [Trait("Module", "2")]
    public class ColorFactoryTests
    {
        /// <summary>
        /// Probar todas las posibilidades de <see cref="ColorFactory.GetColorByName(string)"/> usando el atributo <see cref="ClassDataAttribute"/>.
        /// </summary>
        [Theory]
        [ClassData(typeof(ColorFactoryClassData))]
        public void GetColorByName_WithClassData(Color expected, string name)
        {
            Color color = ColorFactory.GetColorByName(name);

            Assert.Multiple(() =>
            {
                Assert.Equal(expected.R, color.R);
                Assert.Equal(expected.G, color.G);
                Assert.Equal(expected.B, color.B);
                Assert.Equal(expected.A, color.A);
            });
        }

        public static IEnumerable<object[]> ColorsByName => new List<object[]>
        {
            new object[] {Color.Red, "red"},
            new object[] {Color.Blue, "blue"},
            new object[] {Color.Green, "green"},
            new object[] {Color.White, "white"},
            new object[] {Color.Black, "black" }
        };

        /// <summary>
        /// Probar todas las posibilidades de <see cref="ColorFactory.GetColorByName(string)"/> usando el atributo <see cref="MemberDataAttribute"/>.
        /// </summary>
        [Theory]
        [MemberData(nameof(ColorsByName))]
        public void GetColorByName_WithMemberData(Color expected, string name)
        {
            Color color = ColorFactory.GetColorByName(name);

            Assert.Multiple(() =>
            {
                Assert.Equal(expected.R, color.R);
                Assert.Equal(expected.G, color.G);
                Assert.Equal(expected.B, color.B);
                Assert.Equal(expected.A, color.A);
            });
        }

        /// <summary>
        /// Probar que el método <see cref="ColorFactory.GetColorByName(string)"/> devuelve default cuando no es un color conocido.
        /// </summary>
        [Fact]
        public void GetColorByName_ReturnsDefaultIfUnknown()
        {
            Color color = ColorFactory.GetColorByName("Unknown color");

            Assert.Equal(default, color);
        }

        [Theory]
        [ColorCompositionData("PractiseTheories/InputData.json")]
        public void GetColorComposition_JsonSourceCheck(int r, int g, int b)
        {
            Color func() => ColorFactory.GetColorComposition(r, g, b);

            if ((r < 0 || r > 255) || (g < 0 || g > 255) || (b < 0 || b > 255) )
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => func());
            }
            else
            {
                Color color = func();

                Assert.Multiple(() =>
                {
                    Assert.Equal(r, color.R);
                    Assert.Equal(g, color.G);
                    Assert.Equal(b, color.B);
                });
            }

        }

    }
}
