using System;

namespace TestxUnitTraining.Module2.xUnitNet.Practise2.Entities
{
    public class MessageData
    {
        public DateTime Date { get; private set; }
        public Author Author { get; private set; }
        public string Message { get; private set; }

        public MessageData(DateTime date, Author author, string message)
        {
            Date = date;
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Message = !string.IsNullOrWhiteSpace(message) ? message : throw new ArgumentNullException(nameof(message));
        }
    }
}
