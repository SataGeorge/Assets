using Assets.Common.Dtos;
using Assets.UI.Models;
using Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace Assets.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceGateway _serviceGateway;

        public HomeController(ILogger<HomeController> logger, IServiceGateway serviceGateway)
        {
            _logger = logger;
            _serviceGateway = serviceGateway;
        }

        public async Task<IActionResult> Index(int? page)
        {
            _logger.LogInformation("Retrieving assets.");
            IEnumerable<AssetDto> assets = await _serviceGateway.GetAll<AssetDto>();
            IEnumerable<AssetViewModel> assetsViewModel = assets.Select(asset =>
            new AssetViewModel
            {
                Segment = asset.Segment,
                Country = asset.Country,
                Product = asset.Product,
                DiscountBand = asset.DiscountBand.ToString(),
                UnitsSold = asset.UnitsSold,
                ManufacturingPrice = asset.ManufacturingPrice,
                SalePrice = asset.SalePrice,
                Date = asset.Date,

            });

            return View(assetsViewModel.ToPagedList(page ?? 1, 10));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
