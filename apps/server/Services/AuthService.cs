using profisysApp.Data;
using profisysApp.Entities;
using profisysApp.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace profisysApp.Services
{
  public class AuthService
  {
    private readonly DatabaseContext _context;
    private readonly IConfiguration _config;
    private readonly AppSettings _settings;

    public AuthService(DatabaseContext context, IConfiguration config, AppSettings settings)
    {
      _context = context;
      _config = config;
      _settings = settings;
    }

    public async Task<bool> Register(string username, string password, string role = "User")
    {
      if (await UserExists(username))
        return false;

      var user = CreateUser(username, password, role);
      
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      
      return true;
    }

    public async Task<string?> Login(string username, string password)
    {
      var user = await GetUserByUsername(username);
      
      if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
        return null;

      return CreateToken(user);
    }

    private async Task<bool> UserExists(string username)
    {
      return await _context.Users.AnyAsync(u => u.Username == username);
    }

    private async Task<User?> GetUserByUsername(string username)
    {
      return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
    }

    private User CreateUser(string username, string password, string role)
    {
      var (passwordSalt, passwordHash) = CreatePasswordHash(password);

      return new User
      {
        Username = username,
        PasswordHash = passwordHash,
        PasswordSalt = passwordSalt,
        Role = role
      };
    }

    private (byte[], byte[]) CreatePasswordHash(string password)
    {
      using var hmac = new HMACSHA512();
      var passwordSalt = hmac.Key;
      var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

      return (passwordSalt, passwordHash);
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
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
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