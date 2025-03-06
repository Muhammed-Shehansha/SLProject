using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TmsWebApi.Services;
using TechnicianWebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using TmsWebApi.Models;

namespace TmsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet("GetTaskSummary")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> GetTaskSummary()
        {
            try
            {
                _logger.LogInformation("Fetching task summary counts.");

                var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                var userRole = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

                if (userIdClaim == null || userRole == null)
                {
                    return Unauthorized(new { message = "Invalid user credentials." });
                }

                int userId = int.Parse(userIdClaim);
                bool isAdmin = userRole == "Admin";

                var summary = await _taskService.GetTaskSummaryAsync(userId, isAdmin);

                return Ok(summary);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred while fetching task summary.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpGet("GetAllTasks")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("GetTaskById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound(new { message = "Task not found." });

            return Ok(task);
        }

        [HttpPost("AddTask")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTask([FromBody] AddTaskDto taskDto)
        {
            var task = new Tasks
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                AssignedToId = taskDto.AssignedToId,
                Status = "Pending"
            };

            await _taskService.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("UpdateTask/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto updatedTaskDto)
        {
            var updatedTask = new Tasks
            {
                Title = updatedTaskDto.Title,
                Description = updatedTaskDto.Description,
                DueDate = updatedTaskDto.DueDate ?? default,
                AssignedToId = updatedTaskDto.AssignedToId ?? 0,
                Status = updatedTaskDto.Status
            };

            var result = await _taskService.UpdateTaskAsync(id, updatedTask);
            if (!result) return NotFound(new { message = "Task not found." });

            return NoContent();
        }

        [HttpDelete("DeleteTask/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
