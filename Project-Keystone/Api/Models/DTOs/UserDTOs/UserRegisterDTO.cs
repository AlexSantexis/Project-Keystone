using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs.UserDTOs
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name can't be longer than 50 characters")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Last name can't be longer than 50 characters")]
        public string? Lastname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long, contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string? Password { get; set; }

    }
}
