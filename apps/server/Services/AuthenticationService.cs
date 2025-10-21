using profisysApp.Data;
using profisysApp.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using profisysApp.Config;

namespace profisysApp.Services
{

  public class AuthenticationService
  {
    private readonly DatabaseContext _context;
    private readonly IConfiguration _config;
    private readonly AppSettings _settings;

    public AuthenticationService(DatabaseContext context, IConfiguration config, AppSettings settings)
    {
      _settings = settings;
      _context = context;
      _config = config;
    }

    public async Task<bool> Register(string username, string password, string role = "User")
    {
      if (_context.Users.Any(u => u.Username == username))
        return false;

      CreatePasswordHash(password, out byte[] hash, out byte[] salt);

      var user = new User { Username = username, PasswordHash = hash, PasswordSalt = salt, Role = role };
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<string?> Login(string username, string password)
    {
      var user = _context.Users.SingleOrDefault(u => u.Username == username);
      if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
        return null;

      return CreateToken(user);
    }

    private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
      using var hmac = new HMACSHA512();
      salt = hmac.Key;
      hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
      using var hmac = new HMACSHA512(salt);
      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
      return computedHash.SequenceEqual(hash);
    }

    private string CreateToken(User user)
    {
      var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),  // będzie jako "sub"
        new Claim("username", user.Username),                        // będzie jako "name"
        new Claim("role", user.Role)                             // będzie jako "role"
    };
      
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      var token = new JwtSecurityToken(
          claims: claims,
          expires: DateTime.UtcNow.AddHours(_settings.JWT_EXPIRE_TIME),
          signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}