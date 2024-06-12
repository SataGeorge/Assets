using Assets.Common.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;

namespace Common.Tests
{
    public class HttpServiceGatewayTests
    {
        private Mock<IHttpClientFactory>? _httpClientFactory;

        [Fact]
        public async Task GetAll_Returns200k_WithAllAssets()
        {
            HttpServiceGateway _httpServiceGateway = await CreateService(HttpStatusCode.OK);
            IEnumerable<AssetDto> assets = await _httpServiceGateway.GetAll<AssetDto>();
            Assert.NotNull(assets);
            Assert.Equal(3, assets.Count());
        }

        [Fact]
        public async Task GetAll_ReturnsNon200k_ThrowExcetion()
        {
            HttpServiceGateway _httpServiceGateway = await CreateService(HttpStatusCode.BadRequest);
            _ = Assert.ThrowsAsync<Exception>(_httpServiceGateway.GetAll<AssetDto>);
        }

        [Fact]
        public async Task GetAll_ReturnsNonHttpException_ThrowExcetion()
        {
            HttpServiceGateway _httpServiceGateway = await CreateService(HttpStatusCode.OK);
            _ = _httpClientFactory.Setup(cl => cl.CreateClient(HttpServiceGateway.HttpClientName)).Throws<Exception>();
            _ = Assert.ThrowsAsync<Exception>(_httpServiceGateway.GetAll<AssetDto>);
        }

        private async Task<HttpServiceGateway> CreateService(HttpStatusCode statusCode)
        {
            Mock<HttpMessageHandler> messageHandler = new();
            AssetDto[] data = new AssetDto[]
            {
                new() { Country = "UK", DiscountBand = DiscountBandDto.Low, Product = "Onion", Segment = "SomeSegment"},
                new() { Country = "USA", DiscountBand = DiscountBandDto.High, Product = "Tomato", Segment = "SomeSegment"},
                new() { Country = "Germany", DiscountBand = DiscountBandDto.Medium, Product = "Meat", Segment = "SomeSegment" }
            };

            FakeHttpClient<AssetDto> httpClient = new(
                new FakeMessageHandler<AssetDto>(data, statusCode));

            _httpClientFactory = new Mock<IHttpClientFactory>();
            _ = _httpClientFactory.Setup(cl => cl.CreateClient(HttpServiceGateway.HttpClientName)).Returns(httpClient);

            HttpServiceGateway _httpServiceGateway = new(
                _httpClientFactory.Object,
                Options.Create(new EndPoint { GetAllUri = "http://someurl" }),
                Mock.Of<ILogger<HttpServiceGateway>>());

            return await Task.FromResult(_httpServiceGateway);
        }
    }
}