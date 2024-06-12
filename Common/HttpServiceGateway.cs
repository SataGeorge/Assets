using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace Common
{
    public class HttpServiceGateway : IServiceGateway
    {
        public const string HttpClientName = "CommonClient";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<EndPoint> _endPoint;
        private readonly ILogger<HttpServiceGateway> _logger;

        public HttpServiceGateway(IHttpClientFactory httpClientFactory, IOptions<EndPoint> endPoint, ILogger<HttpServiceGateway> logger)
        {
            _httpClientFactory = httpClientFactory;
            _endPoint = endPoint;
            _logger = logger;
        }
        public async Task<IEnumerable<T>> GetAll<T>()
        {
            HttpResponseMessage? responseMessage = null;
            try
            {
                HttpClient client = _httpClientFactory.CreateClient(HttpClientName);
                responseMessage = await client.GetAsync(_endPoint.Value.GetAllUri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Susccessfully retrieved data from endpoint {_endPoint.Value.GetAllUri}");
                    string data = await responseMessage.Content.ReadAsStringAsync();
                    return !string.IsNullOrEmpty(data) ?
                        JsonConvert.DeserializeObject<IEnumerable<T>>(data) ?? Enumerable.Empty<T>() :
                        Enumerable.Empty<T>();
                };

                throw new Exception($"Could not communicate with the endpoint. status code is {responseMessage.StatusCode}");
            }
            catch (Exception)
            {
                _logger.LogError($"Could not communicate with the endpoint. status code is {responseMessage?.StatusCode ?? HttpStatusCode.InternalServerError}");
                throw;
            }
        }
    }
}
