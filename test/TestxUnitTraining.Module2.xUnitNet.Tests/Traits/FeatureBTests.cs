using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestxUnitTraining.Module2.xUnitNet.Tests.Traits.CustomTraits;
using TestxUnitTraining.Module2.xUnitNet.Traits;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Traits
{
    /// <summary>
    /// El atributo trait permite categorizar pruebas por funcionalidades asi poder ejecutarlas o filtrarlas por dicha categoria.
    /// Tambien se puede aplicar a toda la clase y lo toman todos los métodos de su clase
    /// </summary>
    [Trait("Category", "FeatureB")]
    [Trait("Module", "2")]
    public class FeatureBTests
    {
        /// <summary>
        /// Con el atributo bug se simplifica añadirlo a la categoria y poner un id
        /// </summary>
        [Fact]
        [Bug("#1", "#2")]
        public void FeatureBOne_ShouldBeOne()
        {
            var result = FeatureB.FeatureBMethodOne();

            Assert.Equal(1,result);
        }

        /// <summary>
        /// Esto es equivalente al anterior
        /// </summary>
        [Fact]
        [Trait("Category", "Bug")]
        [Trait("Bug", "#1")]
        [Trait("Bug", "#2")]
        public void FeatureBTwo_ShouldBeTwo()
        {
            var result = FeatureB.FeatureBMethodTwo();

            Assert.Equal(2, result);
        }
    }
}
