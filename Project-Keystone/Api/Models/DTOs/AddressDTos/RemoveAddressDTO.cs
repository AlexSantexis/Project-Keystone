using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs.AddressDTos
{
    public class RemoveAddressDTO
    {
        [Required(ErrorMessage = "Address ID is required.")]
        public int AddressId { get; set; }
    }
}
