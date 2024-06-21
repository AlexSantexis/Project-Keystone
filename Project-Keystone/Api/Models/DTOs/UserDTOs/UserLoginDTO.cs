using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs.UserDTOs
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
