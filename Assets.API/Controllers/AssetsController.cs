using Assets.Common.Dtos;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace Assets.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IRepository<AssetDto> _repository;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(IRepository<AssetDto> repository, ILogger<AssetsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<AssetDto>> Get()
        {
            return await _repository.GetAll();
        }
    }
}
