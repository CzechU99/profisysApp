using profisysApp.Entities;
using profisysApp.Config;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using profisysApp.Repositories;

namespace profisysApp.Services
{
  public class AuthService
  {
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;
    private readonly AppSettings _settings;

    public AuthService(IUserRepository userRepository, IConfiguration config, AppSettings settings)
    {
      _userRepository = userRepository;
      _config = config;
      _settings = settings;
    }

    public async Task<bool> Register(string username, string password, string role = "User")
    {
      if (await _userRepository.ExistsUser(username))
        return false;

      var user = _userRepository.CreateUser(username, password, role);

      await _userRepository.AddUser(user);
      await _userRepository.SaveChangesUser();
      
      return true;
    }

    public async Task<string?> Login(string username, string password)
    {
      var user = await _userRepository.GetByUsername(username);
      
      if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
        return null;

      return CreateToken(user);
    }

    private bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
      using var hmac = new HMACSHA512(salt);
      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
      return computedHash.SequenceEqual(hash);
    }

    private string CreateToken(User user)
    {
      var claims = BuildClaims(user);
      var key = GetSigningKey();
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddHours(_settings.JWT_EXPIRE_TIME),
        signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private List<Claim> BuildClaims(User user)
    {
      return new List<Claim>
      {
        new Claim("username", user.Username),
        new Claim("role", user.Role)
      };
    }

    private SymmetricSecurityKey GetSigningKey()
    {
      var keyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
      return new SymmetricSecurityKey(keyBytes);
    }
  }
}