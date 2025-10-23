using profisysApp.Entities;

namespace profisysApp.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> ExistsUserAsync(string username);
        Task<bool> ExistsUserWithRoleAsync(string username, string role);
        Task AddUserAsync(User user);
        Task SaveChangesUserAsync();
        User CreateUser(string username, string password, string role);
    }
}
