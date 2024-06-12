using Assets.Common.Dtos;
using Assets.UI.Controllers;
using Assets.UI.Models;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Assets.UI.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<IServiceGateway> mockService;
        private readonly HomeController controller;

        public HomeControllerTests()
        {
            AssetDto[] viewModel = new AssetDto[]
            {
                new() { Country = "UK", DiscountBand = DiscountBandDto.Low, Product = "Onion", Segment = "SomeSegment"},
                new() { Country = "USA", DiscountBand = DiscountBandDto.High, Product = "Tomato", Segment = "SomeSegment"},
                new() { Country = "Germany", DiscountBand = DiscountBandDto.Medium, Product = "Meat", Segment = "SomeSegment" }
            };

            mockService = new Mock<IServiceGateway>();
            _ = mockService.Setup(svc => svc.GetAll<AssetDto>())
                .ReturnsAsync(viewModel);
            controller = new HomeController(Mock.Of<ILogger<HomeController>>(), mockService.Object);
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAllAssets()
        {
            IActionResult assetsResult = await controller.Index(1);
            ViewResult viewResult = Assert.IsType<ViewResult>(assetsResult);
            X.PagedList.PagedList<AssetViewModel> model = Assert.IsAssignableFrom<X.PagedList.PagedList<AssetViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count);
        }
    }
}