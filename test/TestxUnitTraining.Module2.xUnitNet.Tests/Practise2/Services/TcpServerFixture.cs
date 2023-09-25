using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestxUnitTraining.Module2.xUnitNet.Practise2.Entities;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Practise2.Services
{
    /// <summary>
    /// Fixture para el Servidor TCP. Usa un puerto común y gracias a usar colección las clases de test no se
    /// ejecutarán de forma concurrente. Si esto sucediese, fallaría por tener el mismo puerto y abrirlo dos veces
    /// </summary>
    public class TcpServerFixture : IDisposable
    {
        public const int Port = 43256;
        private readonly TcpServer _tcpServer;
        private string? _lastMessage;

        public TcpServerFixture()
        {
            _tcpServer = new TcpServer();
            _tcpServer.DataReceived += (message) =>
            {
                _lastMessage = message;
            };
        }

        public void Listen() =>
            _tcpServer.Escuchar(Port);

        public void Dispose()
        {
            _tcpServer.Desconectar();
        }

        public T? GetLastMessage<T>() => string.IsNullOrWhiteSpace(_lastMessage) ? default : JsonConvert.DeserializeObject<T>(_lastMessage)!;
    }
}
