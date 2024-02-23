using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Model.Context
{
    public class SQLContext : DbContext
    {
        public SQLContext(){}
        public SQLContext(DbContextOptions<SQLContext> options) : base(options){}
        public DbSet<CartProduct> CartProducts { get; set;}
        public DbSet<CartDetail> CartDetails { get; set;}
        public DbSet<CartHeader> CartHeaders { get; set;}
    }
}
