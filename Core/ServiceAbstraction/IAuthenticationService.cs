using Shared.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> UserExistsAsync(string email);
        Task<UserDto?> GetCurrentUserAsync(string email);
        Task<AddressDto?> GetUserAddressAsync(string userId);
        Task<AddressDto?> UpdateUserAddressAsync(string userId, AddressDto addressDto);
    }
}
