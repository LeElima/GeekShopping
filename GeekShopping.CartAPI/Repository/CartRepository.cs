using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly SQLContext _context;
        private IMapper _mapper;
        public CartRepository(SQLContext sQLContext, IMapper mapper)
        {
            _context = sQLContext;
            _mapper = mapper;
        }
        public Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cartHeader != null)
            {
                _context.CartDetails.RemoveRange(_context.CartDetails.Where(c=>c.CartHeaderId == cartHeader.Id));
                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId),
            };
            cart.CartDetails = _context.CartDetails.Where(c => c.CartHeaderId == cart.CartHeader.Id).Include(c => c.CartProduct);
            return _mapper.Map<CartVO>(cart);
        }

        public Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(c => c.Id == cartDetailsId);
                int total = _context.CartDetails.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();

                _context.CartDetails.Remove(cartDetail);
                if(total == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            throw new NotImplementedException();
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO cart)
        {
            Cart ct = _mapper.Map<Cart>(cart);
            var product = await _context.CartProducts.FirstOrDefaultAsync(p => p.Id == cart.CartDetails.FirstOrDefault().ProductId);
            if (product == null)
            {
                _context.CartProducts.Add(ct.CartDetails.FirstOrDefault().CartProduct);
                await _context.SaveChangesAsync();
            }

            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == ct.CartHeader.UserId);
            if(cartHeader == null)
            {
                _context.CartHeaders.Add(ct.CartHeader);
                await _context.SaveChangesAsync();
                ct.CartDetails.FirstOrDefault().CartHeaderId = ct.CartHeader.Id;
                ct.CartDetails.FirstOrDefault().CartProduct = null;
                _context.CartDetails.Add(ct.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId && p.CartHeaderId == cartHeader.Id);
                if (cartDetail == null)
                {
                    ct.CartDetails.FirstOrDefault().CartHeaderId = ct.CartHeader.Id;
                    ct.CartDetails.FirstOrDefault().CartProduct = null;
                    _context.CartDetails.Add(ct.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ct.CartDetails.FirstOrDefault().CartProduct = null;
                    ct.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    ct.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    ct.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    _context.CartDetails.Update(ct.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartVO>(ct);
        }
    }
}
