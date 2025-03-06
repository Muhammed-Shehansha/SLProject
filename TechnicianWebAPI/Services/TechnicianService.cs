using System.Collections.Generic;
using System.Threading.Tasks;
using TmsWebApi.Models;
using TmsWebApi.Repositories;
using TechnicianWebAPI.DTOs;
using BCrypt.Net;
using Microsoft.Extensions.Logging;

namespace TmsWebApi.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly ITechnicianRepository _technicianRepository;
        private readonly ILogger<TechnicianService> _logger;

        public TechnicianService(ITechnicianRepository technicianRepository, ILogger<TechnicianService> logger)
        {
            _technicianRepository = technicianRepository;
            _logger = logger;
        }

        public async Task<List<User>> GetAllTechniciansAsync()
        {
            return await _technicianRepository.GetAllTechniciansAsync();
        }

        public async Task<User> GetTechnicianByIdAsync(int id)
        {
            return await _technicianRepository.GetTechnicianByIdAsync(id);
        }

        public async Task<bool> TechnicianExistsAsync(string username, string email, int? id = null)
        {
            return await _technicianRepository.TechnicianExistsAsync(username, email, id);
        }

        public async Task<bool> AddTechnicianAsync(AddTechnicianDto technicianDto)
        {
            if (await TechnicianExistsAsync(technicianDto.Username, technicianDto.Email))
            {
                return false;
            }

            var technician = new User
            {
                Username = technicianDto.Username,
                Skills = technicianDto.Skills,
                Password = BCrypt.Net.BCrypt.HashPassword(technicianDto.Password),
                Email = technicianDto.Email,
                PhoneNumber = technicianDto.PhoneNumber,
                Role = "Technician"
            };

            await _technicianRepository.AddTechnicianAsync(technician);
            return true;
        }

        public async Task<bool> UpdateTechnicianAsync(int id, AddTechnicianDto technicianDto)
        {
            var existingTechnician = await _technicianRepository.GetTechnicianByIdAsync(id);
            if (existingTechnician == null || await TechnicianExistsAsync(technicianDto.Username, technicianDto.Email, id))
            {
                return false;
            }

            existingTechnician.Username = technicianDto.Username;
            existingTechnician.Skills = technicianDto.Skills;
            existingTechnician.Email = technicianDto.Email;
            existingTechnician.PhoneNumber = technicianDto.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(technicianDto.Password))
            {
                existingTechnician.Password = BCrypt.Net.BCrypt.HashPassword(technicianDto.Password);
            }

            return await _technicianRepository.UpdateTechnicianAsync(existingTechnician);
        }

        public async Task<bool> DeleteTechnicianAsync(int id)
        {
            return await _technicianRepository.DeleteTechnicianAsync(id);
        }
    }
}
