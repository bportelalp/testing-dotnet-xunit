using System.Threading.Tasks;
using TestxUnitTraining.Module2.xUnitNet.Practise2.Entities;

namespace TestxUnitTraining.Module2.xUnitNet.Practise2.Services
{
    public interface IAuthorSender
    {
        Task SendAuthorAsync(Author author);
    }
}
