using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs.UserDTOs
{
    public class UserDTO
    {
        public string UserId { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
    }
}
