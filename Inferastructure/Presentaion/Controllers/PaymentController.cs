using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IServicesManger servicesManger) :ControllerBase
    {
        [HttpPost("{BasketId}")]
        [Authorize]
        public async Task<IActionResult> ProcessPayment(string BasketId)
        {
          var result = await servicesManger.Payment.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(result);
        }

    }
}
