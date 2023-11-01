using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module5.Advanced.Tests.ApiClientsTests
{
    public class FakeMessageHandler : HttpMessageHandler
    {
        private HttpResponseMessage _response = new();

        public void SetExpectedResponse<T>(HttpStatusCode statusCode, T? content)
        {
            _response = new HttpResponseMessage(statusCode);
            if (content is not null)
            {
                string serialized = JsonConvert.SerializeObject(content);
                StringContent stringContent = new StringContent(serialized);
                _response.Content = stringContent;
            }
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }
}
