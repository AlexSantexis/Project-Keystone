using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs.AddressDTos
{
    public class AddAddressDTO
    {
        [Required(ErrorMessage = "Street address is required.")]
        [StringLength(int.MaxValue, ErrorMessage = "Street address is too long.")]
        public string StreetAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(int.MaxValue, ErrorMessage = "City name is too long.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zip code is required.")]
        [StringLength(int.MaxValue, ErrorMessage = "Zip code is too long.")]
        public string ZipCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(int.MaxValue, ErrorMessage = "Country name is too long.")]
        public string Country { get; set; } = string.Empty;
    }
}
