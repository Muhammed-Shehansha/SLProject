using System.ComponentModel.DataAnnotations;
namespace TechnicianWebAPI.DTOs
{
    public class TaskSummaryDto
    {
        public int TotalTasks { get; set; }
        public int PendingTasks { get; set; }
        public int CompletedTasks { get; set; }
    }
}