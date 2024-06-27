using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs.UserDTOs
{
    public class UserUpdatePasswordDTO
    {
        
        [Required, MinLength(8)]
        public string? currentPassword { get; set; }

        [Required,MinLength(8)]
        public string? newPassword { get; set; }
    }
}
