using Dotby.Application.DTOs;
using Dotby.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Dotby.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CartController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet("{userId}", Name = "GetCart")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _service.CartService.GetCartAsync(userId);
            return Ok(cart);
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToCart(string userId, [FromBody] AddToCartDto addToCartDto)
        {
            var cartItem = await _service.CartService.AddToCartAsync(userId, addToCartDto);
            return CreatedAtRoute("GetCart", new { userId }, cartItem);
        }
        [HttpPut("{userId}/{productId}")]
        public async Task<IActionResult> UpdateCartItem(string userId, Guid productId, [FromBody] UpdateCartItemDto updateCartItemDto)
        {
            var cartItem = await _service.CartService.UpdateCartItemAsync(userId, productId, updateCartItemDto);
            return Ok(cartItem);
        }
        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(string userId, Guid productId)
        {
            await _service.CartService.RemoveFromCartAsync(userId, productId);
            return NoContent();
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            await _service.CartService.ClearCartAsync(userId);
            return NoContent();
        }


    }

}