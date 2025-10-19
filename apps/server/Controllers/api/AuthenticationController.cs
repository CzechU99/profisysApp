using Microsoft.AspNetCore.Mvc;
using profisysApp.Services;
using profisysApp.Models;

namespace profisysApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authService;

        public AuthenticationController(AuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            var success = await _authService.Register(request.Username, request.Password);
            if (!success)
                return BadRequest("Użytkownik już istnieje.");
            return Ok("Rejestracja udana.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
          var token = await _authService.Login(request.Username, request.Password);
          if (token == null)
            return Unauthorized("Błędne dane logowania.");
          return Ok(new { token });
        }
        
    }
}
