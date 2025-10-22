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
      var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
      var documentsRepository = scope.ServiceProvider.GetRequiredService<DocumentsRepository>();

      await SeedDefaultUser(userRepository, "admin", "admin", "Admin");
      await SeedDefaultUser(userRepository, "user", "user", "User");
    }
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

  private async Task SeedDefaultUser(UserRepository _userRepository, string username, string password, string role)
  {
    if (await _userRepository.ExistsUserWithRole(username, role))
      return;

    var user = _userRepository.CreateUser(username, password, role);
    await _userRepository.AddUser(user);
    await _userRepository.SaveChangesUser();
  }
  
}