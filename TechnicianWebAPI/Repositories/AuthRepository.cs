using Microsoft.EntityFrameworkCore;
using TmsWebApi.Data;
using TmsWebApi.Models;
using System.Threading.Tasks;

namespace TmsWebApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
        }
    }
}
