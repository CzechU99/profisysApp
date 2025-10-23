using Microsoft.EntityFrameworkCore;
using profisysApp.Data;
using profisysApp.Entities;
using System.Security.Cryptography;
using System.Text;

namespace profisysApp.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
      _context = context;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
      return await _context.Users
        .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> ExistsUserAsync(string username)
    {
      return await _context.Users
        .AnyAsync(u => u.Username == username);
    }

    public async Task<bool> ExistsUserWithRoleAsync(string username, string role)
    {
      return await _context.Users
        .AnyAsync(u => u.Username == username && u.Role == role);
    }

    public async Task AddUserAsync(User user)
    {
      await _context.Users.AddAsync(user);
    }

    public async Task SaveChangesUserAsync()
    {
      await _context.SaveChangesAsync();
    }

    public User CreateUser(string username, string password, string role)
    {
      var (salt, hash) = CreatePasswordHash(password);

      return new User
      {
        Username = username,
        Role = role,
        PasswordHash = hash,
        PasswordSalt = salt
      };
    }
    
    private (byte[], byte[]) CreatePasswordHash(string password)
    {
      using var hmac = new HMACSHA512();
      var passwordSalt = hmac.Key;
      var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

      return (passwordSalt, passwordHash);
    }
  }
}