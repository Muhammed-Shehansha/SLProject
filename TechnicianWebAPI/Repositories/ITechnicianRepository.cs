using System.Collections.Generic;
using System.Threading.Tasks;
using TmsWebApi.Models;

namespace TmsWebApi.Repositories
{
    public interface ITechnicianRepository
    {
        Task<List<User>> GetAllTechniciansAsync();
        Task<User> GetTechnicianByIdAsync(int id);
        Task<bool> TechnicianExistsAsync(string username, string email, int? id = null);
        Task AddTechnicianAsync(User technician);
        Task<bool> UpdateTechnicianAsync(User technician);
        Task<bool> DeleteTechnicianAsync(int id);
    }
}
