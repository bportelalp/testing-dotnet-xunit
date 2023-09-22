using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Fixtures
{
    /// <summary>
    /// Esta clase define la coleccion que se usará en los tests
    /// </summary>
    [CollectionDefinition("FixtureCollection")]
    public class DirectoryFixtureCollection : ICollectionFixture<DirectoryFixtureForCollection>
    {
    }

    /// <summary>
    /// Esta clase define la coleccion que se usará en los tests
    /// </summary>
    [CollectionDefinition("FixtureCollection2")]
    public class DirectoryFixtureCollection2 : ICollectionFixture<DirectoryFixtureForCollection>
    {
    }
}
