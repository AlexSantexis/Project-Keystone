using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs
{
    public class DeleteUserDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
