using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TestxUnitTraining.Tests.Common.Traits
{

    public class UseFixtureTraitDiscoverer : ITraitDiscoverer
    {
        public static readonly string TraitName = "UseFixture";
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            Type[] fixtureTypes = traitAttribute.GetNamedArgument<Type[]>(nameof(UseFixtureTraitAttribute.FixtureType));

            foreach (Type fixtureType in fixtureTypes)
            {
                yield return new KeyValuePair<string, string>(TraitName, fixtureType.Name);
            }
        }
    }
}
