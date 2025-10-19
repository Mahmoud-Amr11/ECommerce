

using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProduct([FromQuery]ProductQueryParams productQueryParams )
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(productQueryParams);
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<BrandDto>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();

            return Ok(brands);


        }
        [HttpGet("types")]
        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();

            return Ok(types);


        }
    }
}
