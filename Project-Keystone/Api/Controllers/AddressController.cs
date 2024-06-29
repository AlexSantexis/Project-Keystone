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
    [Produces("application/json")]
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

        /// <summary>
        /// Gets the address of the authenticated user.
        /// </summary>
        /// <returns>The user's address.</returns>
        /// <response code="200">Returns the user's address</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="404">If the address is not found</response>
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

        /// <summary>
        /// Adds a new address for the authenticated user.
        /// </summary>
        /// <param name="addressDTO">The address data to add.</param>
        /// <returns>A success message if the address is added successfully.</returns>
        /// <response code="200">Returns success message when address is added</response>
        /// <response code="400">If the address data is invalid or operation fails</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="409">If an address already exists for the user</response>
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

        /// <summary>
        /// Updates the address of the authenticated user.
        /// </summary>
        /// <param name="addressDTO">The updated address data.</param>
        /// <returns>A success message if the address is updated successfully.</returns>
        /// <response code="200">Returns success message when address is updated</response>
        /// <response code="400">If the address data is invalid or operation fails</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="404">If the address is not found</response>
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

        /// <summary>
        /// Removes the address of the authenticated user.
        /// </summary>
        /// <returns>A success message if the address is removed successfully.</returns>
        /// <response code="200">Returns success message when address is removed</response>
        /// <response code="400">If the operation fails</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="404">If the address is not found</response>
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

