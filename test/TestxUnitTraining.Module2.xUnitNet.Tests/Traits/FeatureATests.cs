using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestxUnitTraining.Module2.xUnitNet.Traits;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Traits
{
    /// <summary>
    /// El atributo trait permite categorizar pruebas por funcionalidades asi poder ejecutarlas o filtrarlas por dicha categoria
    /// </summary>
    [Trait("Module", "2")]
    public class FeatureATests
    {

        [Fact]
        [Trait("Category","FeatureA")]
        public void FeatureATrue_ShouldBeTrue()
        {
            var result = FeatureA.FeatureATrue();

            Assert.True(result);
        }

        [Fact]
        [Trait("Category", "FeatureA")]
        public void FeatureATrue_ShouldBeFalse()
        {
            var result = FeatureA.FeatureAFalse();

            Assert.False(result);
        }
    }
}
