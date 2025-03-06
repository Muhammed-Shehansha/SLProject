using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TmsWebApi.Services;
using TechnicianWebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace TmsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechnicianController : ControllerBase
    {
        private readonly ITechnicianService _technicianService;
        private readonly ILogger<TechnicianController> _logger;

        public TechnicianController(ITechnicianService technicianService, ILogger<TechnicianController> logger)
        {
            _technicianService = technicianService;
            _logger = logger;
        }

        [HttpGet("GetAllTechnicians")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTechnicians()
        {
            return Ok(await _technicianService.GetAllTechniciansAsync());
        }

        [HttpGet("GetTechnicianById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTechnicianById(int id)
        {
            var technician = await _technicianService.GetTechnicianByIdAsync(id);
            if (technician == null) return NotFound(new { message = "Technician not found." });

            return Ok(technician);
        }

        [HttpPost("AddTechnician")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTechnician([FromBody] AddTechnicianDto technicianDto)
        {
            if (!await _technicianService.AddTechnicianAsync(technicianDto))
                return Conflict(new { message = "Username or Email already exists!" });

            return Ok(new { message = "Technician added successfully!" });
        }
    }
}
