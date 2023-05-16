using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MultitenantApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger,
            IProductService productService)
        {
            _logger = logger;
            _service = productService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var productDetail = await _service.GetByIdAsync(id);
            return Ok(productDetail);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productDetails = await _service.GetAllAsync();
            return Ok(productDetails);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductsDto request)
        {
            return Ok(await _service.CreateAsync(request.Name, request.Description, request.Rate));
        }

        [HttpPost("CreateWithBackgroundJob")]
        public IActionResult CreateWithBackgroundJob(List<ProductsDto> request)
        {
            return Ok(_service.CreateWithBackgroundJob(request));
        }
    }
}