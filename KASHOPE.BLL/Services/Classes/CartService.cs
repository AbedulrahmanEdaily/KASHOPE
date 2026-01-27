using Azure.Core;
using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.CartRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.CartResponse;
using KASHOPE.DAL.Models;
using KASHOPE.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository,IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<BaseResponse> AddToCartAsync(string userId, AddToCartRequest request)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId);
            if(product is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Product Not Found"
                };
            }
            var itemsInCart = await _cartRepository.GetAllAsync(userId);
            var itemInCart = itemsInCart.FirstOrDefault(i => i.UserId == userId && i.ProductId == request.ProductId);
            var count = itemInCart?.Count ?? 0;
            if (request.Count + count > product.Quantity)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "The quantity is not available in stock"
                };
            }
            if(itemInCart is not null)
            {
                itemInCart.Count += request.Count;
                await _cartRepository.UpdateAsync(itemInCart);                    
                return new BaseResponse
                {
                    Success = true,
                    Message = "Product Added"
                };
            }
            
            var item = request.Adapt<Cart>();
            item.UserId = userId;
            await _cartRepository.CreateAsync(item);
            return new BaseResponse
            {
                Success = true,
                Message = "Product Added"
            };
        }

        public async Task<BaseResponse> ClearCartAsync(string userId)
        {
            await _cartRepository.ClearAsync(userId);
            return new BaseResponse
            {
                Success = true,
                Message = "Cart Cleared"
            };
        }

        public async Task<CartSummaryResponse> GetAllProductsFromCart(string userId)
        {
            var cartItems = await _cartRepository.GetAllAsync(userId);
            var response = new CartSummaryResponse
            {
                Items = cartItems.Select(ci => new CartResponse
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.ProductTranslations?.Select(t=>t.Name).FirstOrDefault()??"",
                    Count = ci.Count,
                    Price = ci.Product.Price,
                }).ToList()
            };
            return response;
        }

        public async Task<BaseResponse> RemoveFromCartAsync(string userId , int productId)
        {
            var itemInCart = await _cartRepository.GetCartItem(userId,productId);
            if(itemInCart is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Item Not Found"
                };
            }
            await _cartRepository.DeleteAsync(itemInCart);
            return new BaseResponse
            {
                Success = true,
                Message = "Item removed from cart successfully"
            };
        }

        public async Task<BaseResponse> UpdateQuantityAsync(string userId, int productId , UpdateQuantityRequest request)
        {
            if(request.Count <= 0)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Count must be greater than zero"
                };
            }
            var itemInCart = await _cartRepository.GetCartItem(userId, productId);
            if (itemInCart is null || itemInCart.Product is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Item Not Found"
                };
            }

            if (request.Count > itemInCart.Product.Quantity)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "The quantity is not available in stock"
                };
            }
            itemInCart.Count = request.Count;
            await _cartRepository.UpdateAsync(itemInCart);
            return new BaseResponse
            {
                Success = true,
                Message = "Quantity Changed"
            };
        }
    }
}
