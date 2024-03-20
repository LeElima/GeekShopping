using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.Model.Context
{
    public class SQLContext : DbContext
    {
        public SQLContext(){}
        public SQLContext(DbContextOptions<SQLContext> options) : base(options){}
        public DbSet<OrderDetail> OrderDetail { get; set;}
        public DbSet<OrderHeader> OrderHeader { get; set;}
    }
}
