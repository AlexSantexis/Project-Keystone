using Project_Keystone.Api.Models.DTOs.AddressDTos;

namespace Project_Keystone.Core.Services.Interfaces
{
    public interface IAddressService
    {
        Task<AddressDTO> GetAddressAsync(string userId);
        Task<bool> AddAddressAsync(string userId, AddAddressDTO addressDTO);
        Task<bool> UpdateAddressAsync(string userId, AddAddressDTO addressDTO);
        Task<bool> RemoveAddressAsync(string userId);
    }
}
