using FootballApp.Entities;
using System.Threading.Tasks;

namespace FootballApp.Repositories
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
