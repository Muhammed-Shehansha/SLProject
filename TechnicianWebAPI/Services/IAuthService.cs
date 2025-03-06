using TechnicianWebAPI.DTOs;
using System.Threading.Tasks;

namespace TmsWebApi.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Token, string Message, string Username, string Role)> LoginAsync(LoginUserDto request);
    }
}
