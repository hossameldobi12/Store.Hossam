using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController (IServicesManger servicesManger):ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult>GetBasketByID(string id)
        {
           var reslut = await servicesManger.Basket.GetBasketAsync(id);
            return Ok(reslut);
        }
        [HttpPost]
        public async Task<IActionResult>UpdateBasket(BasketDto basketDto)
        {
            var result = await servicesManger.Basket.UpdateBasketAsync(basketDto);
            return Ok(result);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await servicesManger.Basket.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
