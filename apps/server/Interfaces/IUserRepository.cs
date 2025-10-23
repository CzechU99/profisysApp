using profisysApp.Entities;

namespace profisysApp.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsername(string username);
        Task<bool> ExistsUser(string username);
        Task<bool> ExistsUserWithRole(string username, string role);
        Task AddUser(User user);
        Task SaveChangesUser();
        User CreateUser(string username, string password, string role);
    }
}
