using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IServiceManager _serviceManager) :ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdate(string basketId)
        {
            var basket =await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(basket);
        }
        [HttpPost("WebHook")]
        public async Task<IActionResult> WebHook()
        {
             var json =  new StreamReader(HttpContext.Request.Body).ReadToEnd();
            await _serviceManager.PaymentService.UpdateOrderPaymentStatusAsync(json, Request.Headers["Stripe-Signature"]);
            return new  EmptyResult();
        }
     }
}
