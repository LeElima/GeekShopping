namespace GeekShopping.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly SQLContext _context;
        private IMapper _mapper;
        public CouponRepository(SQLContext sQLContext, IMapper mapper)
        {
            _context = sQLContext;
            _mapper = mapper;
        }

    }
}
