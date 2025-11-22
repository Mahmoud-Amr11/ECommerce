using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("CreateOrder")]
        [Authorize]
        public async Task<ActionResult> CreateOrder(OrderDto orderDto)
        {
           var email =  User.FindFirstValue(ClaimTypes.Email);
           var order = await _serviceManager.OrderService.CreateOrderAsync(email, orderDto);

        return Ok(order);
        }

        [HttpGet("AllOrders")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _serviceManager.OrderService.GetAllOrdersAsync(email);
            return Ok(orders);
        }

        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDileveryMethods()
        {
            var dileveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(dileveryMethods);
        }
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            
            var order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
    }
}
