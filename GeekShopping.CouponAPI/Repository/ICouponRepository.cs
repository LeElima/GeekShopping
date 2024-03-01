using System.Threading.Tasks;

namespace GeekShopping.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCod(string couponCode);
    }
}
