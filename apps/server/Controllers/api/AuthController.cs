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
        private readonly AuditService _auditService;

        public AuthController(AuthService authService, AuditService auditService)
        {
            _authService = authService;
            _auditService = auditService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var success = await _authService.Register(request.Username, request.Password);
            if (!success)
                return BadRequest(new { message = "Użytkownik już istnieje." });

            await _auditService.LogAsync(request.Username, "Zarejestrowano nowego użytkownika", $"Rola: User");
            return Ok(new { message = "Rejestracja udana." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.Login(request.Username, request.Password);
            if (token == null)
                return Unauthorized(new { message = "Błędne dane logowania." });
            
            await _auditService.LogAsync(request.Username, "Zalogowano do aplikacji");
            return Ok(new { token, message = "Zalogowano pomyślnie!" });
        }
        
    }
}
