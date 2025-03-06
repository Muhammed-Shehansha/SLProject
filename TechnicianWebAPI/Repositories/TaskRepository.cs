using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmsWebApi.Data;
using TmsWebApi.Models;
using Microsoft.Extensions.Logging;
using TechnicianWebAPI.DTOs;

namespace TmsWebApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(ApplicationDbContext context, ILogger<TaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> GetTotalTasksAsync(int? userId)
        {
            var query = _context.Tasks.AsQueryable();
            if (userId.HasValue)
            {
                query = query.Where(task => task.AssignedToId == userId);
            }
            return await query.CountAsync();
        }

        public async Task<int> GetPendingTasksAsync(int? userId)
        {
            var query = _context.Tasks.AsQueryable();
            if (userId.HasValue)
            {
                query = query.Where(task => task.AssignedToId == userId);
            }
            return await query.CountAsync(task => task.Status == "Pending");
        }

        public async Task<int> GetCompletedTasksAsync(int? userId)
        {
            var query = _context.Tasks.AsQueryable();
            if (userId.HasValue)
            {
                query = query.Where(task => task.AssignedToId == userId);
            }
            return await query.CountAsync(task => task.Status == "Completed");
        }

        public async Task<List<TaskDto>> GetAllTasksAsync()
        {
            _logger.LogInformation("Fetching all tasks.");

            var tasks = await _context.Tasks
                .Join(_context.Users,
                      task => task.AssignedToId,
                      user => user.Id,
                      (task, user) => new TaskDto
                      {
                          Id = task.Id,
                          Title = task.Title,
                          Description = task.Description,
                          DueDate = task.DueDate,
                          Status = task.Status,
                          AssignedToId = user.Id,
                          AssignedTechnician = user.Username
                      })
                .ToListAsync();

            return tasks;
        }


        public async Task<Tasks> GetTaskByIdAsync(int taskId)
        {
            _logger.LogInformation("Fetching task with ID: {TaskId}", taskId);
            return await _context.Tasks.FindAsync(taskId);
        }

        public async Task<List<Tasks>> GetTasksByTechnicianIdAsync(int technicianId)
        {
            _logger.LogInformation("Fetching tasks for technician ID: {TechnicianId}", technicianId);
            return await _context.Tasks.Where(t => t.AssignedToId == technicianId).ToListAsync();
        }

        public async Task AddTaskAsync(Tasks task)
        {
            _logger.LogInformation("Adding a new task: {Title}", task.Title);
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTaskAsync(int id, Tasks updatedTask)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null) return false;

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.AssignedToId = updatedTask.AssignedToId;
            existingTask.Status = updatedTask.Status;

            _context.Tasks.Update(existingTask);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTaskStatusAsync(int id, string status)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            task.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
