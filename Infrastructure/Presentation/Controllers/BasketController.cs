using Microsoft.AspNetCore.Mvc;

using ServiceAbstraction;
using Shared.DataTransferObject.BasketDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager _serviceManager): ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddOrUpdate([FromBody] BasketDto basketDto)
        {
            var result = await _serviceManager.BasketService.AddOrUpdateBasketAsync(basketDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var result = await _serviceManager.BasketService.GetBasketAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }



    }
}
