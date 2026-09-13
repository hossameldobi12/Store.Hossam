using Domain.Exceptions.NotFound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController(IServicesManger servicesManger) :ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequestDto orderDto)
        {
          
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
        
            
            var order = await servicesManger.orderService.CreateOrderAsync(orderDto, userEmail);

            return Ok(order);
        }
        [HttpGet]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var deliveryMethods = await servicesManger.orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await servicesManger.orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetOrdersForUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await servicesManger.orderService.GetOrdersByUserEmailAsync(userEmail);
            return Ok(orders);
        }
    }
}
