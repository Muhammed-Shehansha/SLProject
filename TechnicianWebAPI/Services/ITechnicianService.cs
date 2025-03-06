using System.Collections.Generic;
using System.Threading.Tasks;
using TmsWebApi.Models;
using TechnicianWebAPI.DTOs;

namespace TmsWebApi.Services
{
    public interface ITechnicianService
    {
        Task<List<User>> GetAllTechniciansAsync();
        Task<User> GetTechnicianByIdAsync(int id);
        Task<bool> TechnicianExistsAsync(string username, string email, int? id = null);
        Task<bool> AddTechnicianAsync(AddTechnicianDto technicianDto);
        Task<bool> UpdateTechnicianAsync(int id, AddTechnicianDto technicianDto);
        Task<bool> DeleteTechnicianAsync(int id);
    }
}
