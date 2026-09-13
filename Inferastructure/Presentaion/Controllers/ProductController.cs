using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presistance.Attributes;
using Services.Abstractions;
using Shared.SpecificationsParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServicesManger servicesManger) : ControllerBase
    {
        [HttpGet]
        [Cache(100)]
        [Authorize]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecsParam productSpecsParam)
        {
            var result = await servicesManger.Product.GetAllProductAsync(productSpecsParam);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult?> GetProductById(int id)
        {

            var result = await servicesManger.Product.GetProductByIdAsync(id);

            return Ok(result);



        }
        [HttpGet("Types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await servicesManger.Product.GetAllTypesAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("Brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await servicesManger.Product.GetAllBrandsAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }


    }
}
