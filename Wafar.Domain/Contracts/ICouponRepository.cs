using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Commen;
using Wafar.Domain.Entities.Qr;

namespace Wafar.Domain.Interface
{
    public interface ICouponRepository : IGenericRepository<Coupon>
    {
        // هاتلي الكوبون بالكود الفريد بتاعه (مع بيانات المكافأة المرتبطة)
        Task<Coupon?> GetByUniqueCodeAsync(string uniqueCode);

        // تأكد إن الكود ده مستخدم قبل كده ولا لأ (قبل ما نولّد كود جديد)
        Task<bool> ExistsWithCodeAsync(string uniqueCode);

        // كل الكوبونات اللي لسه Active بس انتهت صلاحيتها (للـ Background Job)
        Task<IReadOnlyList<Coupon>> GetExpiredActiveCouponsAsync();

        // كل كوبونات عميل معين
        Task<IReadOnlyList<Coupon>> GetByCustomerAsync(int customerId);
        Task<IReadOnlyList<Coupon>> GetAllWithDetailsAsync();
    }
}