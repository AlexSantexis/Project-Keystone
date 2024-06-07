using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
    }
}
