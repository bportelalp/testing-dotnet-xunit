using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Traits.CustomTraits
{
    [TraitDiscoverer("TestxUnitTraining.Module2.xUnitNet.Tests.Traits.CustomTraits.BugDiscoverer", "TestxUnitTraining.Module2.xUnitNet.Tests")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class BugAttribute : Attribute, ITraitAttribute
    {
        public BugAttribute(params string[] id)
        {
            Id = id;
        }

        public string[] Id { get; private set; }
    }
}
