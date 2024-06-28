namespace Project_Keystone.Api.Models.DTOs.AddressDTos
{
    public class AddressDTO
    {
        public int AddressId { get; set; }
        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string? UserId { get; set; }
    }
}
