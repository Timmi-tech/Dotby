using AutoMapper;
using Dotby.Application.DTOs;
using Dotby.Application.Services.Contracts;
using Dotby.Domain;
using Dotby.Domain.Contracts;
using Dotby.Domain.Entities.Models;

namespace Dotby.Application.Services
{
    internal sealed class CartService : ICartService
    {
        private readonly IRepositoryManager _reposiotry;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CartService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _reposiotry = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CartDto> GetCartAsync(string userId)
        {
            var cart = await _reposiotry.Cart.GetByUserIdAsync(userId, trackChanges: false);
            if (cart == null)
            {
                throw new CartNotFoundException(userId);
            }
            var cartDto = _mapper.Map<CartDto>(cart);
            return cartDto;
        }

        public async Task<CartItemDto> AddToCartAsync(string userId, AddToCartDto addToCartDto)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var product = await ValidateProductAsync(addToCartDto.ProductId);

            var existingItem = await _reposiotry.CartItem.GetCartItemAsync(cart.Id, addToCartDto.ProductId, trackChanges: false);
            if (existingItem != null)
            {
                return await UpdateExistingCartItemAsync(existingItem, addToCartDto.Quantity);
            }

            var newItem = await AddNewCartItemAsync(cart.Id, product, addToCartDto.Quantity);

            return _mapper.Map<CartItemDto>(newItem);
        }

        private async Task<Cart> GetOrCreateCartAsync(string userId)
        {
            var cart = await _reposiotry.Cart.GetByUserIdAsync(userId, trackChanges: false);

            if (cart is not null)
            {
                return cart;
            }
            var newCart = new Cart
            {
                UserId = userId
            };

            _reposiotry.Cart.AddCart(newCart);
            await _reposiotry.SaveAsync();
            return newCart;
        }
        private async Task<Product> ValidateProductAsync(Guid productId)
        {
            var product = await _reposiotry.Product.GetProductAsync(productId, trackChanges: false);
            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }
            return product;
        }
        private async Task<CartItemDto> UpdateExistingCartItemAsync(CartItem existingItem, int quantityToAdd)
        {
            existingItem.Quantity += quantityToAdd;

            _reposiotry.CartItem.UpdateItem(existingItem, trackChanges: true);
            await _reposiotry.SaveAsync();
            return _mapper.Map<CartItemDto>(existingItem);
        }

        private async Task<CartItemDto> AddNewCartItemAsync(Guid cartId, Product product, int quantity)
        {
            var newItem = new CartItem
            {
                CartId = cartId,
                ProductId = product.Id,
                unitPrice = product.Price,
                Quantity = quantity,
            };

            await _reposiotry.CartItem.AddItemAsync(newItem, trackChanges: false);
            await _reposiotry.SaveAsync();
            return _mapper.Map<CartItemDto>(newItem);
        }

        public async Task<CartItemDto> UpdateCartItemAsync(string userId, Guid productId, UpdateCartItemDto updateCartItemDto)
        {
            var cart = await _reposiotry.Cart.GetByUserIdAsync(userId, trackChanges: false);
            if (cart == null)
            {
                throw new CartNotFoundException(userId);
            }
            var existingItem = await _reposiotry.CartItem.GetCartItemAsync(cart.Id, productId, trackChanges: false);
            if (existingItem == null)
            {
                throw new CartItemNotFoundException(productId);
            }

            existingItem.Quantity = updateCartItemDto.Quantity;
            _reposiotry.CartItem.UpdateItem(existingItem, trackChanges: true);
            await _reposiotry.SaveAsync();

            return _mapper.Map<CartItemDto>(existingItem);
        }
        public async Task RemoveFromCartAsync(string userId, Guid productId)
        {
            var cart = await _reposiotry.Cart.GetByUserIdAsync(userId, trackChanges: false);
            if (cart == null)
            {
                throw new CartNotFoundException(userId);
            }

            var existingItem = await _reposiotry.CartItem.GetCartItemAsync(cart.Id, productId, trackChanges: false);
            if (existingItem == null)
            {
                throw new CartItemNotFoundException(productId);
            }

            _reposiotry.CartItem.RemoveItem(existingItem, trackChanges: false);
            await _reposiotry.SaveAsync();
        }
        public async Task ClearCartAsync(string userId)
        {
            var cart = await _reposiotry.Cart.GetByUserIdAsync(userId, trackChanges: false);
            if (cart == null)
            {
                throw new CartNotFoundException(userId);
            }

            await _reposiotry.CartItem.ClearCartAsync(cart.Id, trackChanges: false);
            await _reposiotry.SaveAsync();
        }

    }
}