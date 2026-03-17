using System.Security.Claims;
using DTOs_AutoMapper.DTOs.CartDTO;
using DTOs_AutoMapper.Services.CartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DTOs_AutoMapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) 
        { 
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCartById() {

            var loggedinuser = User.FindFirst(ClaimTypes.NameIdentifier);

            if (loggedinuser == null)
            {
                return Unauthorized("Invalid User");
            }

            int userId = int.Parse(loggedinuser.Value);


            var cart = await _cartService.getcartbyid(userId);
            return Ok(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCart(Cartcreate dto) {
            var loggedinuser = User.FindFirst(ClaimTypes.NameIdentifier);

            if (loggedinuser == null || !int.TryParse(loggedinuser.Value, out int userId))
            {
                return Unauthorized("Invalid user");
            }

            await _cartService.Addcart(dto,userId);
            return Ok(new
            {
                message = "Cart Added Successfully",
                cart = dto
            });
        }

        //Helper method
        private int? GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.TryParse(claim?.Value, out int id) ? id : null;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, Cartupdate dto)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var updatedcart = await _cartService.Updatecart(id,dto,userId.Value);

            if (updatedcart == null)
                return Forbid("You cannot update this cart");

            return Ok(new 
            {
                message = "Cart Updated Successfully",
            });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id) {

            var userId = GetUserId();
            if (userId == null) return Unauthorized();


            try
            {
                await _cartService.Deletecart(id, userId.Value);
                return Ok("Cart Deleted Successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
