using System;
using System.ComponentModel.DataAnnotations;

namespace TmsWebApi.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; } 
        
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } 

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "DueDate is required.")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "DueDate must be in the future.")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "AssignedToId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "AssignedToId must be a positive number.")]
        public int AssignedToId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } 
    }
}

