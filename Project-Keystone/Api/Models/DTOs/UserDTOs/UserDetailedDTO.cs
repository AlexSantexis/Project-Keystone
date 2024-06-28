using Project_Keystone.Api.Models.DTOs.AddressDTos;

namespace Project_Keystone.Api.Models.DTOs.UserDTOs
{
    public class UserDetailedDTO
    {
        public string Id { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public AddressDTO? Address { get; set; } 

        public IList<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
    }
}
