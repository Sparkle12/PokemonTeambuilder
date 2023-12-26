using Microsoft.AspNetCore.Mvc;
using Server.Services;
using SharedLibrary.Requests;
using SharedLibrary.Responses;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthentificationController : ControllerBase
    {
        private readonly IAuthentificationService _authServ;

        public AuthentificationController(IAuthentificationService authServ ) 
        {
            _authServ = authServ;
        }
        [HttpPost("register")]
        public IActionResult Register(AuthentificationRequest request)
        {
            var (succes, content) = _authServ.Register(request.Username, request.Password);

            if (!succes) return BadRequest(content);


            return Ok();
        }

        [HttpPost("login")]

        public IActionResult Login(AuthentificationRequest request)
        {
            var (succes, content) = _authServ.Register(request.Username, request.Password);

            if (!succes) return BadRequest(content);

            return Ok(new AuthentificationResponse() { Token = content});
        }
    }
}
