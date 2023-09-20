using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.S7Fixtures
{
    /// <summary>
    /// Esta clase es un accesorio para crear una serie de archivos en una carpeta al inicio del test y eliminarlos al finalizar.
    /// Es una copia de <see cref="DirectoryFixture"/> para hacer la demo de ejecutar en paralelo, pero cambia de carpeta
    /// </summary>
    public class DirectoryFixtureForCollection : IDisposable
    {
        private const string _directory = "./testsCollection";
        private const int _fileCount = 5;
        private readonly string[] _files = new string[_fileCount];

        public string DirectoryPath => _directory;
        public string[] Files => _files;

        /// <summary>
        /// Este método se va a llamar en el constructor del test. Pero podría haberse llamado en el conwstructor de esta misma clase y sería equivalente.
        /// </summary>
        public void GenerateFiles()
        {
            if(!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }

            for (int i = 0; i < _fileCount; i++)
            {
                var filename = $"{nameof(DirectoryFixture)}-file-{i}.txt";
                _files[i] = filename;
                File.WriteAllText(Path.Combine(_directory, filename), "Foo");
            }
        }

        public void Dispose()
        {
            if (Directory.Exists(_directory))
            {
                Directory.Delete(_directory, true);
            }
        }
    }
}
