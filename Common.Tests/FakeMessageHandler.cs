using Newtonsoft.Json;
using System.Net;

namespace Common.Tests
{
    public class FakeMessageHandler<T> : HttpMessageHandler
    {
        private readonly IEnumerable<T> _data;
        private readonly HttpStatusCode _statusCode;

        public FakeMessageHandler(IEnumerable<T> data, HttpStatusCode statusCode)
        {
            _data = data;
            _statusCode = statusCode;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(_data))
            });
        }
    }
}