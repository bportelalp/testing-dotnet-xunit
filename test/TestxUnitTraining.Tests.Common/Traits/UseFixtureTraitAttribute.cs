using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace TestxUnitTraining.Tests.Common.Traits
{
    /// <summary>
    /// Categoriza los tests por el uso de una determinada fixture.
    /// </summary>
    [TraitDiscoverer("TestxUnitTraining.Tests.Common.Traits.UseFixtureTraitDiscoverer", "TestxUnitTraining.Tests.Common")]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class UseFixtureTraitAttribute : Attribute, ITraitAttribute
    {
        public UseFixtureTraitAttribute(params Type[] fixtureType)
        {
            FixtureType = fixtureType;
        }

        public Type[] FixtureType { get; }
    }
}
