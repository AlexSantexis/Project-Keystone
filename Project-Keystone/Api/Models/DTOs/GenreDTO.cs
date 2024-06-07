using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs
{
    public class GenreDTO
    {
        public int GenreId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
    }
}
