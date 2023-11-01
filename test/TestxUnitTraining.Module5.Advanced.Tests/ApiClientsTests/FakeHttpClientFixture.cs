using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module5.Advanced.Tests.ApiClientsTests
{
    public class FakeHttpClientFixture
    {
        private HttpClient _httpClient;
        private FakeMessageHandler _handler;

        public FakeHttpClientFixture()
        {
            _handler = new FakeMessageHandler();
            _httpClient = new HttpClient(_handler);
            _httpClient.BaseAddress = new Uri("http://fakeChatApi.com/api");
        }

        public HttpClient HttpClient => _httpClient;
        public FakeMessageHandler Handler => _handler;

    }
}
