using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Keystone.Api.Exceptions.AddressExceptions;
using Project_Keystone.Api.Models.DTOs.AddressDTos;
using Project_Keystone.Core.Services.Interfaces;
using System.Security.Claims;

namespace Project_Keystone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        private string GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }
            return userId;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            try
            {
                var userId = GetUserId();
                var address = await _addressService.GetAddressAsync(userId);
                return Ok(address);
            }
            catch (AddressNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddAddress(AddAddressDTO addressDTO)
        {
            try
            {
                var userId = GetUserId();
                await _addressService.AddAddressAsync(userId, addressDTO);
                return Ok(new { message = "Address added successfully" });
            }
            catch (AddressAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (AddressOperationFailedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAddress(AddAddressDTO addressDTO)
        {
            try
            {
                var userId = GetUserId();
                await _addressService.UpdateAddressAsync(userId, addressDTO);
                return Ok(new { message = "Address updated successfully" });
            }
            catch (AddressNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (AddressOperationFailedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> RemoveAddress()
        {
            try
            {
                var userId = GetUserId();
                await _addressService.RemoveAddressAsync(userId);
                return Ok(new { message = "Address removed successfully" });
            }
            catch (AddressNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (AddressOperationFailedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}

