using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TmsWebApi.Data;
using TmsWebApi.Models;

namespace TmsWebApi.Repositories
{
    public class TechnicianRepository : ITechnicianRepository
    {
        private readonly ApplicationDbContext _context;

        public TechnicianRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllTechniciansAsync()
        {
            return await _context.Users.Where(user => user.Role == "Technician").ToListAsync();
        }

        public async Task<User> GetTechnicianByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id && user.Role == "Technician");
        }

        public async Task<bool> TechnicianExistsAsync(string username, string email, int? id = null)
        {
            return await _context.Users.AnyAsync(user => 
                (user.Username == username || user.Email == email) && (id == null || user.Id != id));
        }

        public async Task AddTechnicianAsync(User technician)
        {
            await _context.Users.AddAsync(technician);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTechnicianAsync(User technician)
        {
            _context.Users.Update(technician);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTechnicianAsync(int id)
        {
            var technician = await GetTechnicianByIdAsync(id);
            if (technician == null) return false;

            _context.Users.Remove(technician);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
