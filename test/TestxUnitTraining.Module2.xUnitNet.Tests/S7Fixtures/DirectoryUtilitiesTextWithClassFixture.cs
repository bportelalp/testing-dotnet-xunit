using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.S7Fixtures
{
    /// <summary>
    /// Este test demuestra que usando una <see cref="IClassFixture{TFixture}"/> se puede usar una clase que se use solo para los tests, iniciandose una vez
    /// solo independientemente de cuantos test existan dentro.
    /// </summary>
    public class DirectoryUtilitiesTextWithClassFixture : IClassFixture<DirectoryFixture>
    {
        private readonly DirectoryFixture _fixture;

        public DirectoryUtilitiesTextWithClassFixture(DirectoryFixture fixture)
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
}
