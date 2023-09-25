using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TestxUnitTraining.Module2.xUnitNet.Practise2.Entities;

namespace TestxUnitTraining.Module2.xUnitNet.Practise2.Services
{
    public class AuthorSender : IAuthorSender
    {
        private readonly IPAddress _ipAddress;
        private readonly int _port;
        public AuthorSender(int port, IPAddress ipAddress)
        {
            _port = port;
            _ipAddress = ipAddress;
        }

        public async Task SendAuthorAsync(Author author)
        {
            using (var tcpClient = new TcpClient())
            {
                tcpClient.Connect(_ipAddress, _port);
                using (var writter = new StreamWriter(tcpClient.GetStream()))
                {
                    var json = JsonConvert.SerializeObject(author);
                    await writter.WriteLineAsync(json);
                    await writter.FlushAsync();
                }
                tcpClient.Close();
            }
        }
    }
}
