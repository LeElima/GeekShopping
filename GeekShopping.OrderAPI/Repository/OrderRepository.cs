
using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<SQLContext> _context;
        public OrderRepository(DbContextOptions<SQLContext> sQLContext)
        {
            _context = sQLContext;
        }

        public async Task<bool> AddOrder(OrderHeader header)
        {
            if (header == null) return false;
            header.CouponCode = header.CouponCode == null ? "" : header.CouponCode;
            await using var _db = new SQLContext(_context);
            _db.OrderHeader.Add(header);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid)
        {
            await using var _db = new SQLContext(_context);
            var header = await _db.OrderHeader.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
            if (header != null)
            {
                header.PaymentStatus = paid;
                await _db.SaveChangesAsync();
            }


        }
    }
}
