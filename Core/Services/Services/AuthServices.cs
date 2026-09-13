using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared;
using Domain.Exceptions.NotFound;
using Domain.Exceptions.BadRequest;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Services.Services
{
    public class AuthServices(UserManager<AppUser> userManager, IOptions<JwtOptions> jwtOptions,IMapper mapper) : IAuthService
    {
        public UserManager<AppUser> UserManager { get; } = userManager;

        public async Task<bool> CheckEmailExistAsync(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<AddressDto> GetCurrentAddressAsync(string email)
        {
            var user =  await UserManager.Users.Include(d => d.Address).FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) { throw new UserEmailNotFoundException(email);}
            var addressResult = mapper.Map<AddressDto>(user.Address);
            return addressResult;

        }
        public async Task<AddressDto> UpdateCurrentAddressAsync(AddressDto addressDto, string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null) throw new UserEmailNotFoundException(email);
            var result = mapper.Map<Address>(addressDto);
            user.Address = result;
            await UserManager.UpdateAsync(user);
            return addressDto;
        }

        public async Task<UserResultDto> GetCurrentUserAsync(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null) throw new UserEmailNotFoundException(email);
            var userResult = new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token =  await GenerateJWTTokenAsync(user)
            };
            return userResult;
        }

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await UserManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UnAuthorizedException();
            var result = await UserManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) throw new UnAuthorizedException();
            return new UserResultDto()
            {
                Email = loginDto.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateJWTTokenAsync(user)

            };



        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {

            if (await CheckEmailExistAsync(registerDto.Email))
            {
                throw new DuplicatedEmailBadRequest(registerDto.Email);
            }

            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.phoneNumber

            };
            var result = await UserManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.Select(error => error.Description);
                throw new ValdiationError(error);
            }
            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJWTTokenAsync(user)
            };
        }

     

        private async Task<string> GenerateJWTTokenAsync(AppUser user)
        {

            var options = jwtOptions.Value;

            var authclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Email , user.Email)
            };
            var roles = await UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authclaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var Summetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));


            var token = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: authclaims,
                expires: DateTime.UtcNow.AddDays(options.DurationInDays),
                signingCredentials: new SigningCredentials(Summetrickey, SecurityAlgorithms.HmacSha256Signature)



                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }




    }
}
