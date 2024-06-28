namespace Project_Keystone.Api.Models.DTOs.AddressDTos
{
    public class AddressDetailedDTO
    {
        public int AddressId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
