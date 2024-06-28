using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Project_Keystone.Api.Exceptions.AddressExceptions;
using Project_Keystone.Api.Models.DTOs.AddressDTos;
using Project_Keystone.Core.Entities;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Core.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddressService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddAddressAsync(string userId, AddAddressDTO addressDTO)
        {
            var existingAddress = await _unitOfWork.Address.GetAddressByUserIdAsync(userId);
            if (existingAddress != null)
            {
                throw new AddressAlreadyExistsException(userId);
            }

            var address = _mapper.Map<Address>(addressDTO);
            address.UserId = userId;

            await _unitOfWork.Address.AddAsync(address);
            var result = await _unitOfWork.CommitAsync();
            if (result <= 0)
            {
                throw new AddressOperationFailedException("add");
            }
            return true;
        }

        public async Task<AddressDTO> GetAddressAsync(string userId)
        {
            var address = await _unitOfWork.Address.GetAddressByUserIdAsync(userId);
            if (address == null)
            {
                throw new AddressNotFoundException(userId);
            }
            return _mapper.Map<AddressDTO>(address);
        }

        public async Task<bool> RemoveAddressAsync(string userId)
        {
            var address = await _unitOfWork.Address.GetAddressByUserIdAsync(userId);
            if (address == null)
            {
                throw new AddressNotFoundException(userId);
            }

            var removed = await _unitOfWork.Address.DeleteAsync(address.AddressId);
            if (!removed)
            {
                throw new AddressOperationFailedException("remove");
            }

            var result = await _unitOfWork.CommitAsync();
            if (result <= 0)
            {
                throw new AddressOperationFailedException("remove");
            }
            return true;
        }

        public async Task<bool> UpdateAddressAsync(string userId, AddAddressDTO addressDTO)
        {
            var existingAddress = await _unitOfWork.Address.GetAddressByUserIdAsync(userId);
            if (existingAddress == null)
            {
                var address = _mapper.Map<Address>(addressDTO);
                address.UserId = userId;
                await _unitOfWork.Address.AddAsync(address);
            }
            else
            {
                _mapper.Map(addressDTO, existingAddress);
                _unitOfWork.Address.UpdateAsync(existingAddress);
            }

            var saveResult = await _unitOfWork.Address.SaveChangesAsync();
            return saveResult > 0;
        }
    }

}
