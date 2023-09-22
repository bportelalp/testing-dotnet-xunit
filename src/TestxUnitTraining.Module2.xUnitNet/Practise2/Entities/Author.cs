using System;

namespace TestxUnitTraining.Module2.xUnitNet.Practise2.Entities
{
    public class Author
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Author(int id, string name)
        {
            Id = id > 0 ? id : throw new ArgumentNullException(nameof(id));
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }
    }
}
