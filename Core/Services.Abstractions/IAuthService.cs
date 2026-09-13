using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthService
    {
        Task<UserResultDto>LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> CheckEmailExistAsync(string email);
        Task<UserResultDto> GetCurrentUserAsync(string email);
        Task<AddressDto> GetCurrentAddressAsync(string email);
        Task<AddressDto> UpdateCurrentAddressAsync (AddressDto addressDto, string email);
    }
}
