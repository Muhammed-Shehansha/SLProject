using System.ComponentModel.DataAnnotations;
namespace TechnicianWebAPI.DTOs
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "DueDate must be in the future.")]
        public DateTime? DueDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "AssignedToId must be a positive number.")]
        public int? AssignedToId { get; set; }
        public string? Status { get; set; }
    }
}