using GeekShopping.CouponAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Model.Context
{
    public class SQLContext : DbContext
    {
        public SQLContext(){}
        public SQLContext(DbContextOptions<SQLContext> options) : base(options){}
        public DbSet<Coupon> Coupon { get; set;}
    }
}
