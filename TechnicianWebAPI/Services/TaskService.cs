using System.Collections.Generic;
using System.Threading.Tasks;
using TmsWebApi.Models;
using TmsWebApi.Repositories;
using Microsoft.Extensions.Logging;
using TechnicianWebAPI.DTOs;

namespace TmsWebApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<object> GetTaskSummaryAsync(int userId, bool isAdmin)
    {
        int? filterUserId = isAdmin ? null : userId;

        var summary = new
        {
            TotalTasks = await _taskRepository.GetTotalTasksAsync(filterUserId),
            PendingTasks = await _taskRepository.GetPendingTasksAsync(filterUserId),
            CompletedTasks = await _taskRepository.GetCompletedTasksAsync(filterUserId)
        };

        return summary;
    }

        public async Task<List<TaskDto>> GetAllTasksAsync() => await _taskRepository.GetAllTasksAsync();

        public async Task<Tasks> GetTaskByIdAsync(int taskId) => await _taskRepository.GetTaskByIdAsync(taskId);

        public async Task<List<Tasks>> GetTasksByTechnicianIdAsync(int technicianId) => await _taskRepository.GetTasksByTechnicianIdAsync(technicianId);

        public async Task AddTaskAsync(Tasks task) => await _taskRepository.AddTaskAsync(task);

        public async Task<bool> UpdateTaskAsync(int id, Tasks updatedTask) => await _taskRepository.UpdateTaskAsync(id, updatedTask);

        public async Task<bool> UpdateTaskStatusAsync(int id, string status) => await _taskRepository.UpdateTaskStatusAsync(id, status);

        public async Task DeleteTaskAsync(int id) => await _taskRepository.DeleteTaskAsync(id);
    }
}
