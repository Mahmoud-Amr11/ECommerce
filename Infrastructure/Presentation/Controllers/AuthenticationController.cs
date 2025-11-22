using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDto;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto request)
        {
            var user = await _serviceManager.AuthenticationService.LoginAsync(request);
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto request)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(request);
            return Ok(user);

        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
        {
            var result = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(user);
        }

        [Authorize]
        [HttpPost("Address")]
        public async Task<ActionResult<UserDto>> GetUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _serviceManager.AuthenticationService.GetUserAddressAsync(email);
            return Ok(user);

        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<UserDto>> UpdateUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _serviceManager.AuthenticationService.UpdateUserAddressAsync(email, address);
            return Ok(user);
        }


        public IActionResult Index()
        {
            return Ok("Authentication Controller is working!");

        }
    }
}