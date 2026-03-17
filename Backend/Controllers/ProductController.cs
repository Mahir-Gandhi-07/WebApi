using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using DTOs_AutoMapper.Filters;
using DTOs_AutoMapper.Models;
using DTOs_AutoMapper.Services.ProductService;
using DTOs_AutoMapper.DTOs.ProductDTO;

namespace DTOs_AutoMapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(LogActionFilter))]
    public class ProductController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger, IMemoryCache memoryCache )
        {
            _memoryCache = memoryCache;
            _productService = productService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int? page, [FromQuery] int? pageSize)
        {        
            string cacheKey;

            List<Productread> products;

            if (page.HasValue && pageSize.HasValue)
            {
                cacheKey = $"Products-page{page}-size{pageSize}";
                if (!_memoryCache.TryGetValue(cacheKey, out products))
                {
                    Console.WriteLine("DATA COMING FROM DATABASE (PAGINATED)");
                    products =await _productService.GetAllProducts(page.Value, pageSize.Value);

                    if (products == null || !products.Any())
                        return NotFound("No products found");

                    _memoryCache.Set(cacheKey, products, TimeSpan.FromMinutes(5));
                }
                else
                {
                    Console.WriteLine("DATA COMING FROM CACHE (PAGINATED)");
              }
            }
            else
            {
                cacheKey = "Products-All";
                if (!_memoryCache.TryGetValue(cacheKey, out products))
                {
                    Console.WriteLine("DATA COMING FROM DATABASE (ALL PRODUCTS)");
                    products = await _productService.GetAllProducts(); 

                    if (products == null || !products.Any())
                        return NotFound("No products found");

                    _memoryCache.Set(cacheKey, products, TimeSpan.FromMinutes(5));
                }
                else
                {
                    Console.WriteLine("DATA COMING FROM CACHE (ALL PRODUCTS)");
                }
            }

            return Ok(products);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducyById([FromRoute] int id)
        {
            var product = await _productService.GetProductById(id);
            _logger.LogInformation($"Fetching the data with id = {id}");
            if (product == null)
            {
                _logger.LogWarning("No Product Found");
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Productcreate dto)
        {
            await _productService.Add(dto);
            return Ok(new
            {
                message = " product added successfully",
                product = dto
            });
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] Productupdate dto)
        {
            var existingProduct = await _productService.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            await _productService.Update(id, dto);
            return Ok(dto);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
                return NotFound();

            await _productService.Delete(id);
            return Ok("Product Deleted");
        }
    }
}
