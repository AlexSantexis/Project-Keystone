namespace Project_Keystone.Core.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        public string StreetAddress { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string? UserId { get; set; }
        public  User User { get; set; } = null!;
    }
}
