using System.ComponentModel.DataAnnotations;
using Constants;
namespace TechnicianWebAPI.DTOs
{
    public class AddTechnicianDto
    {
        public int? Id { get; set; }
        
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 16 characters.")]
        [RegularExpression(ModelConstants.UserNamePattern, ErrorMessage = ModelConstants.InvalidUserNameMessage)]
        public string Username { get; set; }

        [RegularExpression(ModelConstants.PasswordPattern, ErrorMessage = ModelConstants.InvalidPasswordMessage)]
        public string? Password { get; set; }

        [Required]
        [RegularExpression(ModelConstants.EmailPattern, ErrorMessage = ModelConstants.InvalidEmailMessage)]
        public string Email { get; set; }

        public string? Skills { get; set; }

        [Required]
        [RegularExpression(ModelConstants.PhoneNumberPattern, ErrorMessage = ModelConstants.InvalidPhoneNumberMessage)]
        public string PhoneNumber { get; set; }
    }
}