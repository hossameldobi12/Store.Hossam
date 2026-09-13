using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dto;
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
    public class AuthController(IServicesManger servicesManger) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await servicesManger.AuthService.LoginAsync(loginDto);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
           var result = await servicesManger.AuthService.RegisterAsync(registerDto);
            return Ok(result);
        }

        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await servicesManger.AuthService.GetCurrentUserAsync(email);
            return Ok(result);
        }


        [HttpGet("CheckEmailExist")]
        public async Task<IActionResult> CheckEmailExist(string email)
        {
            var result = await servicesManger.AuthService.CheckEmailExistAsync(email);
            return Ok(result);
        }


        [HttpGet("GetCurrentAddress")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await servicesManger.AuthService.GetCurrentAddressAsync(email);
            return Ok(result);
        }


        [HttpPut()]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();
            var result = await servicesManger.AuthService.UpdateCurrentAddressAsync(addressDto, email);
            return Ok(result);
        }


    }
}
