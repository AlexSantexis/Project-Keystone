using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs
{
    public class RoleDTO
    {
        public int RoleId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;
    }
}
