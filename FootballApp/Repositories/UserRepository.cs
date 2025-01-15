using FootballApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FootballApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FootballDbContext _context;

        public UserRepository(FootballDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
