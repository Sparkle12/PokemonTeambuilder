using Microsoft.AspNetCore.Mvc;
using Server.Repository;
using Server.Services;
using SharedLibrary.Requests;
using SharedLibrary.Responses;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authServ;

        public AuthenticationController(IAuthenticationService authServ ) 
        {
            _authServ = authServ;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthenticationRequest request)
        {
            var (succes, content) = _authServ.Register(request.Username, request.Password);

            if (!succes) return BadRequest(content);


            return Ok();
        }

        [HttpPost("login")]

        public async Task<AuthenticationResponse> Login(AuthenticationRequest request)
        {
            var (succes, content) = _authServ.Login(request.Username, request.Password);

            return new AuthenticationResponse() { Token = content};
        }
    }
}
