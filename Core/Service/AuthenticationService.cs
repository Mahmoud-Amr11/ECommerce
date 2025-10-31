using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
           var user=await  _userManager.FindByEmailAsync(loginDto.Email);

            if(user is null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }
            var result=  await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (result)
            {
                return new UserDto
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = "This is a token"
                };
            }
            throw new UnauthorizedAccessException();
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user= new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber= registerDto.PhoneNumer
            };
            var result= await _userManager.CreateAsync(user, registerDto.Password);
            if(result.Succeeded)
            {
                return new UserDto
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = "This is a token"
                };
            }
            else
            {
                var errors= result.Errors.Select(e=>e.Description).ToList();
                throw new BadRequestException(errors);
            }
        }
    }
}
