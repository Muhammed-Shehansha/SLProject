using System.ComponentModel.DataAnnotations;

namespace TmsWebApi.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        public string Message { get; set; }

        public bool IsRead { get; set; } = false;

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; } // Foreign key to User
    }
}
