using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities;
using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Enum;
using Wafar.Infrastructure.Migrations.Data.Migration;

namespace Wafar.Infrastructure.Repositories
{
    public class CommissionRepository : GenericRepository<Commission> ,ICommissionRepository
    {
        public CommissionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Commission>> GetByPartnerAsync(int partnerId, DateTime? from, DateTime? to)
        {
            var query = _context.Commissions
                .Include(c => c.QRCode)
                .Include(c => c.Coupon)
                .Where(c => c.PartnerId == partnerId);

            if (from.HasValue) query = query.Where(c => c.CreatedAt >= from.Value);
            if (to.HasValue) query = query.Where(c => c.CreatedAt <= to.Value);

            return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task<decimal> GetTotalPendingAmountAsync(int partnerId)
        {
            return await _context.Commissions
                .Where(c => c.PartnerId == partnerId && c.Status == CommissionStatus.Pending)
                .SumAsync(c => c.CalculatedAmount);
                
        }
    }
}
