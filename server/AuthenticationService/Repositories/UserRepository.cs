using ScreenOps.AuthenticationService.Data;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Models;

namespace ScreenOps.AuthenticationService.Repositories
{
    public class UserRepository:IUserRepository
    {
        private AppDBContext _context;

        public UserRepository(AppDBContext context) { 
            _context = context;
        }

        public async Task<User?> FindByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> Insert(User user)
        {
            _context.Users.Add(user);
            await SaveChanges();
            return user;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
