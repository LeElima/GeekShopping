using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IService;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace GeekShopping.Web.Services
{
    public class CartService : ICartService
    {
        public Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            throw new NotImplementedException();
        }
        public Task<CartViewModel> AddItemToCart(CartViewModel cart, string token)
        {
            throw new NotImplementedException();
        }
        public Task<CartViewModel> UpdateCart(CartViewModel cart, string token)
        {
            throw new NotImplementedException();
        }
        public Task<bool> RemoveFromCart(long cartId, string token)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string token)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        

        public Task<bool> RemoveCoupon(string userId, string token)
        {
            throw new NotImplementedException();
        }

        

        
    }
}
