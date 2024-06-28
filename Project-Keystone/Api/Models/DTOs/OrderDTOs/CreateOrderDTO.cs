using Project_Keystone.Api.Models.DTOs.AddressDTos;

namespace Project_Keystone.Api.Models.DTOs.OrderDTOs
{
    public class CreateOrderDTO
    {
        public string UserEmail { get; set; } = string.Empty;
        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
