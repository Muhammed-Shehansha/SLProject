using TmsWebApi.Models;

namespace TechnicianWebAPI.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public int AssignedToId { get; set; }
        public string? AssignedTechnician { get; set; }
    }
}

