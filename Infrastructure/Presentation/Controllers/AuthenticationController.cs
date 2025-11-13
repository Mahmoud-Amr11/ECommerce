using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.IdentityDto;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {


        [HttpPost("Login")]
        public ActionResult<UserDto> Login(LoginDto request)
        {
             var user=_serviceManager.AuthenticationService.LoginAsync(request);
                return Ok(user);
        }

        [HttpPost("Register")]
        public ActionResult<UserDto> Register(RegisterDto request)
        {
            var user = _serviceManager.AuthenticationService.RegisterAsync(request);
            return Ok(user);
            
        }
    }
}
