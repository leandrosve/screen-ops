using AuthenticationService.Models;

namespace ScreenOps.AuthenticationService.Repositories
{
    public interface IUserRepository
    {
        Task<User?> FindByEmail(string email);

        Task<User?> GetById(Guid guid);

        Task<User> Insert(User user);

    }
}
