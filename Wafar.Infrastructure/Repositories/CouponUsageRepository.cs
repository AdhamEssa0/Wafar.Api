using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Commen;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Interface;
using Wafar.Infrastructure.Migrations.Data.Migration;

namespace Wafar.Infrastructure.Repositories
{
    public class CouponUsageRepository : GenericRepository<CouponUsage> ,ICouponUsageRepository
    {
        public CouponUsageRepository(AppDbContext context) : base(context) { }
    }
}
