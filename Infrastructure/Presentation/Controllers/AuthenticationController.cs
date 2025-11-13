using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.IdentityDto;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto request)
        {
             var user=await _serviceManager.AuthenticationService.LoginAsync(request);
                return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto request)
        {
            var user =await _serviceManager.AuthenticationService.RegisterAsync(request);
            return Ok(user);
            
        }
    }
}
