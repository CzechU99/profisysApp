using profisysApp.Repositories;

public class DatabaseSeeder : IHostedService
{
  private readonly IServiceProvider _serviceProvider;

  public DatabaseSeeder(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    using (var scope = _serviceProvider.CreateScope())
    {
      var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
      var documentsRepository = scope.ServiceProvider.GetRequiredService<IDocumentsRepository>();

      await SeedDefaultUserAsync(userRepository, "admin", "admin", "Admin");
      await SeedDefaultUserAsync(userRepository, "user", "user", "User");
    }
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

  private async Task SeedDefaultUserAsync(IUserRepository _userRepository, string username, string password, string role)
  {
    if (await _userRepository.ExistsUserWithRoleAsync(username, role))
      return;

    var user = _userRepository.CreateUser(username, password, role);
    await _userRepository.AddUserAsync(user);
    await _userRepository.SaveChangesUserAsync();
  }
  
}