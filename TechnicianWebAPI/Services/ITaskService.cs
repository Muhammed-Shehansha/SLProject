using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicianWebAPI.DTOs;
using TmsWebApi.Models;

namespace TmsWebApi.Services
{
    public interface ITaskService
    {
        Task<object> GetTaskSummaryAsync(int userId, bool isAdmin);
        Task<List<TaskDto>> GetAllTasksAsync();
        Task<Tasks> GetTaskByIdAsync(int taskId);
        Task<List<Tasks>> GetTasksByTechnicianIdAsync(int technicianId);
        Task AddTaskAsync(Tasks task);
        Task<bool> UpdateTaskAsync(int id, Tasks updatedTask);
        Task<bool> UpdateTaskStatusAsync(int id, string status);
        Task DeleteTaskAsync(int id);
    }
}
