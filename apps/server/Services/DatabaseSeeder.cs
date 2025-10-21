using profisysApp.Data;
using profisysApp.Entities;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

public class DatabaseSeeder : IHostedService
{
  private readonly IServiceProvider _serviceProvider;

  public DatabaseSeeder(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    using var scope = _serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    await SeedDefaultUser(context);
    await SeedDefaultAdmin(context);
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

  private async Task SeedDefaultUser(DatabaseContext context)
  {
    if (!await context.Users.AnyAsync(u => u.Username == "user" && u.Role == "User"))
    {
      var (salt, hash) = CreatePasswordHash("user");

      var normalUser = new User
      {
        Username = "user",
        Role = "User",
        PasswordHash = hash,
        PasswordSalt = salt
      };
      context.Users.Add(normalUser);
    }

    await context.SaveChangesAsync();
  }

  private async Task SeedDefaultAdmin(DatabaseContext context)
  {
    if (!await context.Users.AnyAsync(u => u.Username == "admin" && u.Role == "Admin"))
    {
      var (salt, hash) = CreatePasswordHash("admin");

      var adminUser = new User
      {
        Username = "admin",
        Role = "Admin",
        PasswordHash = hash,
        PasswordSalt = salt
      };
      context.Users.Add(adminUser);
    }

    await context.SaveChangesAsync();
  }
  
  private (byte[], byte[]) CreatePasswordHash(string password)
  {
    using var hmac = new HMACSHA512();
    var salt = hmac.Key;
    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

    return (salt, hash);
  }
}