using TmsWebApi.Models;
using System.Threading.Tasks;

namespace TmsWebApi.Repositories
{
    public interface IAuthRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
