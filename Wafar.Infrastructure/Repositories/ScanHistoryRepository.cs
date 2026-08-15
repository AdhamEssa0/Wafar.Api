using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Coupons;
using Wafar.Infrastructure.Migrations.Data.Migration;

namespace Wafar.Infrastructure.Repositories
{
    public class ScanHistoryRepository : GenericRepository<ScanHistory> , IScanHistoryRepository
    {
        public ScanHistoryRepository(AppDbContext context) : base(context) { }

        public async Task<int> CountByCodeAsync(int qrCodeId)
        {
            return await _context.ScanHistories.CountAsync(s => s.QRCodeId == qrCodeId);
        }

        public async Task<IEnumerable<ScanHistory>> GetByCustomerAsync(int customerId)
        {
            return await _context.ScanHistories
                .Include(s => s.QRCode)
                .Where(s => s.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
