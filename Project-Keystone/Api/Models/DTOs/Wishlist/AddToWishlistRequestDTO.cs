using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Project_Keystone.Api.Models.DTOs.Wishlist
{
    public class AddToWishlistRequestDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid ProductId")]
        public int ProductId { get; set; }
    }
}
