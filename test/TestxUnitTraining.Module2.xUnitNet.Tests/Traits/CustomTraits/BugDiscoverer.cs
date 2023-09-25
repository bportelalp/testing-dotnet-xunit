using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Traits.CustomTraits
{
    public class BugDiscoverer : ITraitDiscoverer
    {
        /// <summary>
        /// Todos los tests que lleven el atributo bug se "redirecciona" como categoría bug y además se añade
        /// un id de bug a ese mismo
        /// </summary>
        /// <param name="traitAttribute"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            string[] bugIds = traitAttribute.GetNamedArgument<string[]>(nameof(BugAttribute.Id));

            yield return new KeyValuePair<string, string>("Category", "Bug");

            foreach (var bugId in bugIds)
            {
                if (!string.IsNullOrWhiteSpace(bugId))
                {
                    yield return new KeyValuePair<string, string>("Bug", bugId);
                }
            }
        }
    }
}
