using Microsoft.AspNetCore.Mvc;
using profisysApp.Services;
using profisysApp.Models;

namespace profisysApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var success = await _authService.Register(request.Username, request.Password);
            if (!success)
                return BadRequest(new { message = "Użytkownik już istnieje." });
            return Ok(new { message = "Rejestracja udana." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
          var token = await _authService.Login(request.Username, request.Password);
          if (token == null)
            return Unauthorized(new { message = "Błędne dane logowania." });
          return Ok(new { token, message = "Zalogowano pomyślnie!" });
        }
        
    }
}
