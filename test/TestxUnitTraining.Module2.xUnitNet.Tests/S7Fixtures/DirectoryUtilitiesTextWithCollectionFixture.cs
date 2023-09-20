using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.S7Fixtures
{
    /// <summary>
    /// Esta clase usa una coleccion, que es común con otras que usen la misma colección.
    /// Dentro usa <see cref="DirectoryFixtureForCollection"/>, igual que en la siguiente clase.
    /// Pero como son dos colecciones distintas a las que se hace referencia el nombre pues
    /// pueden ejecutarse en paralelo
    /// </summary>
    [Collection("FixtureCollection")]
    public class DirectoryUtilitiesTextWithCollectionFixture
    {
        private readonly DirectoryFixtureForCollection _fixture;

        public DirectoryUtilitiesTextWithCollectionFixture(DirectoryFixtureForCollection fixture)
        {
            _fixture = fixture;
            _fixture.GenerateFiles();
        }

        [Fact]
        public void FilesExist_ShouldBeTrue_IfExist()
        {
            var result = DirectoryUtilities.FilesExist(_fixture.Files, _fixture.DirectoryPath);

            Assert.True(result);
        }

        [Fact]
        public void FilesExits_ShouldBeFalse_IfOtherDirectory()
        {
            var result = DirectoryUtilities.FilesExist(_fixture.Files, "./");

            Assert.False(result);
        }

        [Fact]
        public void FIlesExist_ShouldBeFalse_IfDifferentFiles()
        {
            var result = DirectoryUtilities.FilesExist(new string[] {"File1", "File2"}, _fixture.DirectoryPath);

            Assert.False(result);
        }
    }

    /// <summary>
    /// Esta clase usa una coleccion, que es común con otras que usen la misma colección
    /// Prueba a cambiarlo por FixtureCollection2. Verás que falla porque se ejecutan en paralelo.
    /// Usando la misma coleción, los tests con la misma colección no se ejecutan en paralelo.
    /// </summary>
    [Collection("FixtureCollection")]
    public class DirectoryUtilitiesTextWithCollectionFixture2
    {
        private readonly DirectoryFixtureForCollection _fixture;

        public DirectoryUtilitiesTextWithCollectionFixture2(DirectoryFixtureForCollection fixture)
        {
            _fixture = fixture;
            _fixture.GenerateFiles();
        }

        [Fact]
        public void FilesExist_ShouldBeTrue_IfExist()
        {
            var result = DirectoryUtilities.FilesExist(_fixture.Files, _fixture.DirectoryPath);

            Assert.True(result);
        }

        [Fact]
        public void FilesExits_ShouldBeFalse_IfOtherDirectory()
        {
            var result = DirectoryUtilities.FilesExist(_fixture.Files, "./");

            Assert.False(result);
        }

        [Fact]
        public void FIlesExist_ShouldBeFalse_IfDifferentFiles()
        {
            var result = DirectoryUtilities.FilesExist(new string[] { "File1", "File2" }, _fixture.DirectoryPath);

            Assert.False(result);
        }
    }
}
